using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIPS_Emulator
{
    public partial class Form1 : Form
    {
        private emulator MIPS_emulator;
        public Form1()
        {
            InitializeComponent(); 
        }

        private void InitializeBTN_Click(object sender, EventArgs e)
        {
            string instruction = codeTXT.Text.ToString();
            uint pc = Convert.ToUInt32(PCTXT.Text.ToString(), 10);
            MIPS_emulator = new emulator(instruction,pc);
            fill_MIPS_DGV();
            fill_pipeline_DGV();
        }

        public void fill_MIPS_DGV()
        {
            mips_reg_DGV.Rows.Clear();
            mips_reg_DGV.Rows.Add("$0", MIPS_emulator.mips_reg[0]);
            for (int i = 1; i < MIPS_emulator.mips_reg.Length; i++)
            {
                mips_reg_DGV.Rows.Add("$" + i, MIPS_emulator.mips_reg[i]);
            }
        }
        public void fill_pipeline_DGV()
        {
            pip_reg_DGV.Rows.Clear();

            string if_id = ""+ MIPS_emulator.m_if_id_reg.newPC + "" + MIPS_emulator.m_if_id_reg.instruction;
            pip_reg_DGV.Rows.Add("IF/ID", if_id);

            string id_ex = "" + MIPS_emulator.m_id_ex_reg.newPC + "" + MIPS_emulator.m_id_ex_reg.rs +""+ MIPS_emulator.m_id_ex_reg.rt + "" + MIPS_emulator.m_id_ex_reg.extended_address;
            pip_reg_DGV.Rows.Add("ID/EX", id_ex);

            string ex_mem = "" + MIPS_emulator.m_ex_mem_reg.branched_address + "" + MIPS_emulator.m_ex_mem_reg.zeroFlag + "" + MIPS_emulator.m_ex_mem_reg.instruction_result+""+ MIPS_emulator.m_ex_mem_reg.rt;
            pip_reg_DGV.Rows.Add("EX/MEM", ex_mem);

            string mem_wb = ""+ MIPS_emulator.m_mem_wb_reg.read_data + "" + Convert.ToString(MIPS_emulator.m_mem_wb_reg.instruction_result);
            pip_reg_DGV.Rows.Add("MEM/WB", mem_wb);
        }

       
        private void runCycleBTN_Click(object sender, EventArgs e)
        {
            MIPS_emulator.cycle();
            fill_MIPS_DGV();
            fill_pipeline_DGV();
        }
    }
}
