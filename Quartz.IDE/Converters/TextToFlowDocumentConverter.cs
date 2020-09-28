using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using Quartz.IDE.Parsing;
using ReactiveUI;

namespace Quartz.IDE.Converters
{
    public class TextToFlowDocumentConverter : DependencyObject, IBindingTypeConverter
    {
        private readonly Lazy<Markdown> mMarkdown = new Lazy<Markdown>(() => new Markdown());

        // Using a DependencyProperty as the backing store for Markdown. This enables animation,
        // styling, binding, etc...
        public static readonly DependencyProperty MarkdownProperty =
            DependencyProperty.Register("Markdown", typeof(Markdown), typeof(TextToFlowDocumentConverter), new PropertyMetadata(null));

        public Markdown Markdown
        {
            get => (Markdown)this.GetValue(MarkdownProperty);
            set => this.SetValue(MarkdownProperty, value);
        }

        public int GetAffinityForObjects(Type fromType, Type toType) => fromType == typeof(string) && toType == typeof(FlowDocument)
                ? 10
                : fromType == typeof(FlowDocument) && toType == typeof(string) ? 10 : 0;

        /// <summary>
        /// Tries to convert the text to a <see cref="FlowDocument"/>.
        /// </summary>
        /// <param name="from">
        /// The text to convert from.
        /// </param>
        /// <param name="toType">
        /// The type to convert to.
        /// </param>
        /// <param name="conversionHint">
        /// The conversion hint.
        /// </param>
        /// <param name="result">
        /// The result to out.
        /// </param>
        public bool TryConvert(object? from, Type toType, object? conversionHint, out object? result)
        {
            if (from is null)
            {
                result = null;
                return true;
            }

            string text = (string)from;

            Markdown engine = this.Markdown ?? this.mMarkdown.Value;

            result = engine.Transform(text);
            return true;
        }
    }
}