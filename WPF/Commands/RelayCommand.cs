using System.Windows.Input;

namespace RentCarWPF.Commands;

/// <summary>
/// RelayCommand – design pattern pentru comenzi in WPF MVVM (Lab 9).
/// Implementeaza ICommand si redirectioneaza executia catre metode externe.
/// </summary>
public class RelayCommand : ICommand
{
    private readonly Action<object?> execute;
    private readonly Func<object?, bool>? canExecute;

    public event EventHandler? CanExecuteChanged;

    public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
    {
        this.execute    = execute;
        this.canExecute = canExecute;
    }

    /// <summary>Determina daca butonul este activ.</summary>
    public bool CanExecute(object? parameter)
        => canExecute == null || canExecute(parameter);

    /// <summary>Executa actiunea comenzii.</summary>
    public void Execute(object? parameter)
        => execute(parameter);

    /// <summary>Notifica WPF sa reinterogeze CanExecute.</summary>
    public void Refresh()
        => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
