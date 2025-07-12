grammar Gramatica;

// A regra inicial do parser: uma expressão ou atribuição seguida pelo fim do arquivo (EOF).
start : statement EOF;

statement
    : ID '=' expr           # Atribuicao
    | expr                  # Expressao
    ;

expr
    : expr ('*'|'/') expr   # Multiplicacao_Divisao
    | expr ('+'|'-') expr   # Soma_Subtracao
    | NUMBER                # Numero
    | ID                    # Variavel
    | '(' expr ')'          # Parenteses
    ;

// Regras do lexer
ID     : [a-zA-Z_][a-zA-Z0-9_]* ; // Identificadores para variáveis
NUMBER : [0-9]+ ('.' [0-9]+)? ;   // Suporta números inteiros e decimais
MUL    : '*' ;
DIV    : '/' ;
ADD    : '+' ;
SUB    : '-' ;
ASSIGN : '=' ;
WS     : [ \t\r\n]+ -> skip ;     // Ignora espaços em branco (white space)