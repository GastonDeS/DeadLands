using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    void Execute();

    // Patrón Memento para deshacer acciones
    // void Undo();
}
