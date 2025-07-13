# REPL-ANTLR

Uma calculadora REPL (Read-Eval-Print Loop) implementada em C# usando ANTLR para parsing de expressões matemáticas.

## Funcionalidades

- Operações matemáticas básicas: adição (+), subtração (-), multiplicação (*), divisão (/)
- Suporte a variáveis
- Suporte a números inteiros e decimais
- Parênteses para controle de precedência
- Comandos especiais integrados

## Pré-requisitos

- .NET 9.0 ou superior
- ANTLR 4.9.3 (incluído automaticamente via NuGet)

## Compilação

```bash
dotnet build
```

## Execução

```bash
dotnet run
```

## Como usar

### Operações básicas

```
calc> 2 + 3
5
calc> 10 * 2.5
25
calc> (5 + 3) * 2
16
```

### Variáveis

```
calc> x = 10
10
calc> y = 20
20
calc> x + y
30
calc> resultado = x * y
200
```

### Comandos especiais

- `vars` - Lista todas as variáveis definidas
- `clear` - Limpa todas as variáveis
- `quit` - Sai do programa

### Exemplo de sessão completa

```
calc> a = 5
5
calc> b = 3
3
calc> c = a + b * 2
11
calc> vars
a = 5
b = 3
c = 11
calc> clear
Variáveis limpas.
calc> quit
```

## Estrutura do projeto

- `grammar/Gramatica.g4` - Gramática ANTLR
- `src/Program.cs` - Programa principal e loop REPL
- `src/GramaticaCalculatorVisitor.cs` - Visitor para avaliação de expressões
- `src/VariableManager.cs` - Gerenciamento de variáveis
- `src/ErrorHandler.cs` - Tratamento de erros 