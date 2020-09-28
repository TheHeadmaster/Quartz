using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Quartz.IDE.Controls
{
    /// <summary>
    /// Represents a bindable <see cref="DataGridColumn"/> that hosts a <see cref="ComboBox"/> in
    /// its cells.
    /// </summary>
    public class BindableDataGridComboBoxColumn : DataGridComboBoxColumn
    {
        private void CopyItemsSource(FrameworkElement element) => BindingOperations.SetBinding(element, ComboBox.ItemsSourceProperty,
              BindingOperations.GetBinding(this, ComboBox.ItemsSourceProperty));

        /// <summary>
        /// Gets a combo box control that is bound to the column's <see
        /// cref="DataGridComboBoxColumn.SelectedItemBinding"/>, <see
        /// cref="DataGridComboBoxColumn.SelectedValueBinding"/>, <see
        /// cref="DataGridComboBoxColumn.TextBinding"/> values.
        /// </summary>
        /// <param name="cell">
        /// The cell that will contain the generated element.
        /// </param>
        /// <param name="dataItem">
        /// The data item represented by the row that contains
        /// </param>
        /// <returns>
        /// </returns>

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            FrameworkElement element = base.GenerateEditingElement(cell, dataItem);
            this.CopyItemsSource(element);
            return element;
        }

        /// <summary>
        /// Gets a read-only combo box control that is bound to the column's <see
        /// cref="DataGridComboBoxColumn.SelectedItemBinding"/>, <see
        /// cref="DataGridComboBoxColumn.SelectedValueBinding"/>, <see
        /// cref="DataGridComboBoxColumn.TextBinding"/> values.
        /// </summary>
        /// <param name="cell">
        /// The cell that will contain the generated element.
        /// </param>
        /// <param name="dataItem">
        /// The data item represented by the row that contains the intended cell.
        /// </param>

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            FrameworkElement element = base.GenerateElement(cell, dataItem);
            this.CopyItemsSource(element);
            return element;
        }
    }
}