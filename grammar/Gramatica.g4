grammar Gramatica;

// A regra inicial do parser: uma expressão seguida pelo fim do arquivo (EOF).
start : expr EOF;

expr
    : expr ('*'|'/') expr   # Multiplicacao_Divisao
    | expr ('+'|'-') expr   # Soma_Subtracao
    | NUMBER                   # Numero
    | '(' expr ')'             # Parenteses
    ;

// Regras do lexer
NUMBER : [0-9]+ ('.' [0-9]+)? ; // Suporta números inteiros e decimais
MUL    : '*' ;
DIV    : '/' ;
ADD    : '+' ;
SUB    : '-' ;
WS     : [ \t\r\n]+ -> skip ;  // Ignora espaços em branco (white space)