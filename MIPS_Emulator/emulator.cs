using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MIPS_Emulator
{
    #region pipeline reg
    struct if_id_reg
    {
        public uint newPC;//=old+4   old is from 15-0 in instruction
        public string instruction;//32bit
    }
    struct id_ex_reg
    {
        public CU_WB cu_wb;
        public CU_M cu_m;
        public CU_EX cu_ex;
        public uint newPC;
        public string rs;//21-25 in instruction
        public string rt;//16-20 in instruction
        public string extended_address;//old=16bit new=32bit
        public string rd;//11-15 in instruction
    }
    struct ex_mem_reg
    {
        public CU_WB cu_wb;
        public CU_M cu_m;
        public uint branched_address;// = shift extended_address left by 2 + the newPC
        public short zeroFlag; //if =1 jump to the branched_address
        public int instruction_result;
        public string rt;
        public string rt_or_rd;//if r format =rd of id_ex_reg else X
    }
    struct mem_wb_reg
    {
        public CU_WB cu_wb;
        public string read_data;
        public int instruction_result;
        public string rt_rd;
    }

    #region control signal 
    struct CU_WB
    {
        public short regWrite;
        public short memToReg;
    }
    struct CU_M
    {
        public short branch;
        public short memWrite;
        public short memRead;
        public short pcSrc;
    }
    struct CU_EX
    {
        public short aluSrc;
        public short regDist;
        public short aluOp1;
        public short aluOp0;
    }
    #endregion

    #endregion

    class emulator
    {
        public int[] mips_reg;
        private uint PC;
        private Dictionary<uint, string> instruction_set;
        public if_id_reg m_if_id_reg;
        public id_ex_reg m_id_ex_reg;
        public ex_mem_reg m_ex_mem_reg;
        public mem_wb_reg m_mem_wb_reg;
        private Queue<uint> f_Q, d_Q, ex_Q, mem_Q, wb_Q;
        private string funct;

        public emulator(string instruction,uint PC)
        {
            instruction_set = new Dictionary<uint, string>();
            mips_reg = new int[32];

            //intialize queues
            f_Q = new Queue<uint>();
            d_Q = new Queue<uint>();
            ex_Q = new Queue<uint>();
            mem_Q = new Queue<uint>();
            wb_Q = new Queue<uint>();

            string input_inst = instruction;
            this.PC = PC;

            //fill instruction set
            string[] arr = input_inst.Split('\n');
            foreach (String s in arr)
            {

                uint pc = Convert.ToUInt32(s.Split(':')[0], 10);
                string inst = s.Split(':')[1];
                f_Q.Enqueue(pc);
                instruction_set[pc] = inst;
            }
            //fill mips_reg
            mips_reg[0] = 0;
            for (int i = 1; i < mips_reg.Length; i++)
            {
                mips_reg[i] = i + 100;
            }
            //intialize pipeline reg
            m_if_id_reg = new if_id_reg();
            m_id_ex_reg = new id_ex_reg();
            m_ex_mem_reg = new ex_mem_reg();
            m_mem_wb_reg = new mem_wb_reg();

        }

        //stages
        #region fitch region
        private void fitch()//PC =1000 in first
        {
            if (instruction_set.Keys.Contains(PC))
            {
                m_if_id_reg.instruction = instruction_set[PC];
                PC += 4;
                m_if_id_reg.newPC = PC;
            }
        }

        #endregion

        #region decode region
        private void decode()
        {//EX  00000010 00110010 01000000 00100000
            string inst = m_if_id_reg.instruction.Replace(" ", "");//to remove spaces
            string opcode = inst.Substring(0, 6);
            string rs = inst.Substring(6, 5);
            string rt = inst.Substring(11, 5);
            //i type
            string rd = inst.Substring(16, 5);
            string shamt = inst.Substring(21, 5);
            funct = inst.Substring(26, 6);
            //r type
            string address = inst.Substring(16, 16);
            //

            //fill id/ex reg
            control_unit(opcode);
            m_id_ex_reg.newPC = m_if_id_reg.newPC;
            m_id_ex_reg.extended_address = sign_extend(address);
            m_id_ex_reg.rd = rd;
            register_file(rs, rt);
        }
        private void control_unit(string opcode)//asign control signal to the pipeline reg 
        {
            if (opcode.Contains("1"))
            {
                //I-type
                m_id_ex_reg.cu_ex.aluOp1 = 0;
                m_id_ex_reg.cu_ex.aluOp0 = 1;
                m_id_ex_reg.cu_ex.aluSrc = 0;
                m_id_ex_reg.cu_ex.regDist = -1;
                m_id_ex_reg.cu_m.branch = 1;
                m_id_ex_reg.cu_m.memRead = 0;
                m_id_ex_reg.cu_m.memWrite = 0;
                m_id_ex_reg.cu_wb.memToReg = -1;
                m_id_ex_reg.cu_wb.regWrite = 0;

            }
            else
            {
                //R-type
                m_id_ex_reg.cu_ex.aluOp1 = 1;
                m_id_ex_reg.cu_ex.aluOp0 = 0;
                m_id_ex_reg.cu_ex.aluSrc = 0;
                m_id_ex_reg.cu_ex.regDist = 1;
                m_id_ex_reg.cu_m.branch = 0;
                m_id_ex_reg.cu_m.memRead = 0;
                m_id_ex_reg.cu_m.memWrite = 0;
                m_id_ex_reg.cu_wb.memToReg = 0;
                m_id_ex_reg.cu_wb.regWrite = 1;

            }
        }
        private void register_file(string rs, string rt)
        {
            m_id_ex_reg.rs = rs;
            m_id_ex_reg.rt = rt;
        }

        private string sign_extend(string _16bit_address)
        {
            string t = "";
            for (int i = 0; i < 16; i++)
            {
                t += _16bit_address[0];
            }
            return t + _16bit_address;
        }
        #endregion

        #region Execute region
        private void execute()
        {

            //fill pipeline reg in the ex/mem reg
            m_ex_mem_reg.cu_m.branch = m_id_ex_reg.cu_m.branch;//cu_m
            m_ex_mem_reg.cu_m.memRead = m_id_ex_reg.cu_m.memRead;
            m_ex_mem_reg.cu_m.memWrite = m_id_ex_reg.cu_m.memWrite;
            m_ex_mem_reg.cu_wb.memToReg = m_id_ex_reg.cu_wb.memToReg;//cu_wb
            m_ex_mem_reg.cu_wb.regWrite = m_id_ex_reg.cu_wb.regWrite;
            //
            string cu_aluOp = m_id_ex_reg.cu_ex.aluOp1 + "" + m_id_ex_reg.cu_ex.aluOp0;
            short cu_alusrc = m_id_ex_reg.cu_ex.aluSrc;
            short cu_rigdist = m_id_ex_reg.cu_ex.regDist;

            uint newpc = m_id_ex_reg.newPC;
            string extended_address = m_id_ex_reg.extended_address;
            string rs = m_id_ex_reg.rs;
            string rt = m_id_ex_reg.rt;
            string rd = m_id_ex_reg.rd;

            int offset = shift2Left(extended_address);//shift
            uint branched_Address = address_adder(newpc, offset);//adder
            string aluSrc_Mux_res = aluSrc_MUX(rt, extended_address, cu_alusrc);//alusrc mux
            int ALU_result = ALU(rs, aluSrc_Mux_res, cu_aluOp);//ALU
            string rigdist_Mux_res = aluSrc_MUX(rt, rd, cu_rigdist);//rigdist mux

            //fill the rest of the ex/mem reg 
            m_ex_mem_reg.branched_address = branched_Address;
            if (ALU_result == 0)
                m_ex_mem_reg.zeroFlag = 1;
            else
                m_ex_mem_reg.zeroFlag = 0;
            m_ex_mem_reg.instruction_result = ALU_result;
            m_ex_mem_reg.rt = m_id_ex_reg.rt;
            m_ex_mem_reg.rt_or_rd = rigdist_Mux_res;

        }
        private int shift2Left(string extendedAddress)
        {
            extendedAddress += "00";
            return Convert.ToInt32(extendedAddress, 2);
        }
        private uint address_adder(uint pc, int offset)
        {
            return pc + Convert.ToUInt32(offset);
        }
        private string aluSrc_MUX(string rt, string extendedAddress, short aluSrc)
        {
            if (aluSrc == 0)
                return rt;
            else
                return extendedAddress;
        }
        private string rigdist_MUX(string rt, string rd, short rigdist)
        {
            if (rigdist == 1)
                return rd;
            else
                return rd;
        }
        private int ALU(string rs, string rt, string aluOp)
        {
            int rs_data = mips_reg[Convert.ToInt32(rs, 2)];
            int rt_data = mips_reg[Convert.ToInt32(rt, 2)];
            if (aluOp == "01")//i-type
            {
                return rs_data - rt_data;
            }
            else if (aluOp == "10")//r-type
            {
                if (funct.Equals("100000"))//add
                {
                    return rs_data + rt_data;
                }
                else if (funct.Equals("100010"))//sub
                {
                    return rs_data - rt_data;
                }
                else if (funct.Equals("100100"))//and
                {
                    return rs_data & rt_data;
                }
                else if (funct.Equals("100101"))//or
                {
                    return rs_data | rt_data;
                }
            }
            return -1;
        }
        #endregion

        #region memory access region
        private void memoryAccess()
        {
            //fill pipeline reg in the mem/wb reg
            m_mem_wb_reg.cu_wb.memToReg = m_ex_mem_reg.cu_wb.memToReg;//cu_wb
            m_mem_wb_reg.cu_wb.regWrite = m_ex_mem_reg.cu_wb.regWrite;
            //
            uint branched_address = m_ex_mem_reg.branched_address;
            short cu_branch = m_ex_mem_reg.cu_m.branch;
            short Z_flag = m_ex_mem_reg.zeroFlag;
            short PCSRC = Convert.ToInt16(Z_flag & cu_branch);//and gate
            PC = PCSrc_MUX(PC, branched_address, PCSRC);//the new instruction that have to be excuted


            int inst_result = m_ex_mem_reg.instruction_result;
            string rt = m_ex_mem_reg.rt;

            m_mem_wb_reg.read_data = data_memory(m_ex_mem_reg.cu_m.memWrite, m_ex_mem_reg.cu_m.memRead, inst_result, rt);
            m_mem_wb_reg.instruction_result = inst_result;
            m_mem_wb_reg.rt_rd = m_ex_mem_reg.rt_or_rd;//rd in r-formate dont care in i-formate

        }
        private uint PCSrc_MUX(uint pc, uint branched_address, short PCSrc)
        {
            if (PCSrc == 0)
                return pc;
            else
                return branched_address;
        }
        private string data_memory(short mem_write, short mem_read, int result, string rt)
        {
            //mem_write and mem read is always 0, then no data store so..
            return "0";
        }
        #endregion

        #region write back region
        private void writeBack()
        {
            short cu_regwrite = m_mem_wb_reg.cu_wb.regWrite;
            short cu_mem_to_reg = m_mem_wb_reg.cu_wb.memToReg;
            string read_data = m_mem_wb_reg.read_data;//nop
            int instruction_Result = m_mem_wb_reg.instruction_result;
            string rd = m_mem_wb_reg.rt_rd;//=rd

            int mem_to_reg_MUX_val = mem_to_reg_MUX(read_data, instruction_Result, cu_mem_to_reg);
            if (mem_to_reg_MUX_val != -100000000)// is r-type
                register_file(rd, instruction_Result, cu_regwrite);

        }
        private int mem_to_reg_MUX(string read_data, int inst_result, short cu_mem_to_reg)
        {
            if (cu_mem_to_reg == 0)
            {
                return inst_result;
            }
            else
                return -100000000;
        }
        private void register_file(string write_rd, int write_data, short reg_write)
        {
            if (reg_write == 1)
            {
                int rd = Convert.ToInt32(write_rd, 2);
                mips_reg[rd] = write_data;
            }

        }
        #endregion

        public void cycle()
        {
            if (wb_Q.Count != 0)
            {
                writeBack();
                uint pc = wb_Q.Dequeue();
            }
            if (mem_Q.Count != 0)
            {
                memoryAccess();
                uint pc = mem_Q.Dequeue();
                wb_Q.Enqueue(pc);
            }
            if (ex_Q.Count != 0)
            {
                execute();
                uint pc = ex_Q.Dequeue();
                mem_Q.Enqueue(pc);
            }
            if (d_Q.Count != 0)
            {
                decode();
                uint pc = d_Q.Dequeue();
                ex_Q.Enqueue(pc);
            }
            if (f_Q.Count != 0)
            {
                fitch();
                uint pc = f_Q.Dequeue();
                d_Q.Enqueue(pc);
            }
        }








    }
}
