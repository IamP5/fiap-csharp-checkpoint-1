# Checkpoint 1 — Calculadora em Console C#

Aplicação de console em C# para realizar as operações básicas:
- Adição
- Subtração
- Multiplicação
- Divisão (com tratamento de divisão por zero)

## Integrantes

| Nome              | RM     |
| ----------------- | ------ |
| Bruno Dominicheli | 554981 |
| Gabriel Gouvea    | 555528 |
| Miguel Kapicius   | 556198 |
| Thiago Ferreira   | 555608 |

## Requisitos atendidos

- [x] Exibe o título: **"Calculadora (Adição / Subtração / Multiplicação / Divisão)"**
- [x] Exibe menu com opções de 1 a 5
- [x] Solicita dois números para as operações
- [x] Calcula e exibe o resultado
- [x] Mantém execução em loop até escolher **5-Sair**
- [x] Trata erro de divisão por zero
- [x] Trata entrada inválida de opção e números

## Como executar

### Pré-requisitos
- .NET SDK 8.0+

### Rodando o projeto
```bash
cd fiap-checkpoint1-calculadora
dotnet Program.cs
```

## Evidências (prints)

### 1) Tela com menu carregado
![Menu da calculadora](docs/prints/menu.png)

### 2) Evidência de teste — Adição
![Teste adição](docs/prints/adicao.png)

### 3) Evidência de teste — Subtração
![Teste subtração](docs/prints/subtracao.png)

### 4) Evidência de teste — Multiplicação
![Teste multiplicação](docs/prints/multiplicacao.png)

### 5) Evidência de teste — Divisão
![Teste divisão](docs/prints/divisao.png)

### 6) Evidência de teste - Divisão por zero
![Teste divisão por zero](docs/prints/divisao_por_zero.png)

## Exemplo de uso

```text
Calculadora (Adição / Subtração / Multiplicação / Divisão)

Escolha uma opção:
1-Adição
2-Subtração
3-Multiplicação
4-Divisão
5-Sair
```
