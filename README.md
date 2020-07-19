# MIPS Pipelined Emulator
 simple C# program to emulate MIPS pipelined datapath and control unit and show the value of each register

the test case in general
1000: beq $2, $2, 7                   
1004: or $2, $3, $Zero 
1008: add $5, $3, $4 
1012: and $6, $3, $Zero
1016: add $9, $7, $8
1032: sub $8, $2, $3

the machine code that should be input of the program

1000: 000100 00010 00010 0000000000000111 
1004: 000000 00011 00000 00010 00000 100101 
1008: 000000 00011 00100 00101 00000 100000 
1012: 000000 00011 00000 00110 00000 100100 
1016: 000000 00111 01000 01001 00000 100000 
1032: 000000 00010 00011 01000 00000 100010

