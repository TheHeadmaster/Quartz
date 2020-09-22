using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Librarium.Commands;
using Microsoft.CSharp;

namespace Quartz.IDE.Commands
{
    /// <summary>
    /// Compiles the project from source and runs a debugger-attached instance of the compiled game.
    /// </summary>
    public class CompileAndRunCommand : RelayCommand
    {
        public static ICommand Instance { get; } = new CompileAndRunCommand();

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            ICodeCompiler codeCompiler = CodeDomProvider.CreateProvider()
        }
    }
}