using System.Globalization;

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

const string appTitle = "Calculadora (Adição / Subtração / Multiplicação / Divisão)";

while (true)
{
    Console.Clear();
    Console.WriteLine(appTitle);
    Console.WriteLine();
    Console.WriteLine("Escolha uma opção:");
    Console.WriteLine($"{(int)MenuOption.Adição}-{MenuOption.Adição}");
    Console.WriteLine($"{(int)MenuOption.Subtração}-{MenuOption.Subtração}");
    Console.WriteLine($"{(int)MenuOption.Multiplicação}-{MenuOption.Multiplicação}");
    Console.WriteLine($"{(int)MenuOption.Divisão}-{MenuOption.Divisão}");
    Console.WriteLine($"{(int)MenuOption.Sair}-{MenuOption.Sair}");
    Console.WriteLine();
    Console.Write("Opção: ");

    var optionInput = Console.ReadLine();

    if (!Enum.TryParse(optionInput, out MenuOption option) || !Enum.IsDefined(option))
    {
        ShowMessage("Opção inválida. Digite um número de 1 a 5.");
        continue;
    }

    if (option == MenuOption.Sair)
    {
        Console.WriteLine("Encerrando... até a próxima!");
        break;
    }

    var firstNumber = ReadNumber("Digite o primeiro número: ");
    var secondNumber = ReadNumber("Digite o segundo número: ");

    var operationResult = ExecuteOperation(option, firstNumber, secondNumber);

    if (!operationResult.Success)
    {
        ShowMessage(operationResult.ErrorMessage!);
        continue;
    }

    Console.WriteLine();
    Console.WriteLine($"Resultado: {operationResult.Result}");
    Console.WriteLine();
    Console.Write("Pressione ENTER para voltar ao menu...");
    Console.ReadLine();
}

static decimal ReadNumber(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        var input = Console.ReadLine();

        if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out var number))
        {
            return number;
        }

        Console.WriteLine("Valor inválido. Digite um número válido (ex.: 10 ou 10.5).\n");
    }
}

static OperationResult ExecuteOperation(MenuOption option, decimal firstNumber, decimal secondNumber)
{
    return option switch
    {
        MenuOption.Adição        => OperationResult.Ok(firstNumber + secondNumber),
        MenuOption.Subtração     => OperationResult.Ok(firstNumber - secondNumber),
        MenuOption.Multiplicação => OperationResult.Ok(firstNumber * secondNumber),
        MenuOption.Divisão when secondNumber == 0 => OperationResult.Fail("Erro: divisão por zero não é permitida."),
        MenuOption.Divisão       => OperationResult.Ok(firstNumber / secondNumber),
        _ => OperationResult.Fail("Opção de operação inválida.")
    };
}

static void ShowMessage(string message)
{
    Console.WriteLine();
    Console.WriteLine(message);
    Console.WriteLine();
    Console.Write("Pressione ENTER para continuar...");
    Console.ReadLine();
}

internal enum MenuOption { Adição = 1, Subtração, Multiplicação, Divisão, Sair }

internal sealed record OperationResult(bool Success, decimal? Result, string? ErrorMessage)
{
    public static OperationResult Ok(decimal result) => new(true, result, null);
    public static OperationResult Fail(string message) => new(false, null, message);
}
