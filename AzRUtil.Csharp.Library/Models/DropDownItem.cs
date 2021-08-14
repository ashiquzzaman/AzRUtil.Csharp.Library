namespace AzRUtil.Csharp.Library.Models
{
    public class DropDownItem
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DropDownItem"/>.
        /// </summary>
        public DropDownItem() { }

        /// <summary>
        /// Initializes a new instance of <see cref="DropDownItem"/>.
        /// </summary>
        /// <param name="text">The display text of this <see cref="DropDownItem"/>.</param>
        /// <param name="value">The value of this <see cref="DropDownItem"/>.</param>
        public DropDownItem(string text, string value)
            : this()
        {
            Text = text;
            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="DropDownItem"/>.
        /// </summary>
        /// <param name="text">The display text of this <see cref="DropDownItem"/>.</param>
        /// <param name="value">The value of this <see cref="DropDownItem"/>.</param>
        /// <param name="selected">Value that indicates whether this <see cref="DropDownItem"/> is selected.</param>
        public DropDownItem(string text, string value, bool selected)
            : this(text, value)
        {
            Selected = selected;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="DropDownItem"/>.
        /// </summary>
        /// <param name="text">The display text of this <see cref="DropDownItem"/>.</param>
        /// <param name="value">The value of this <see cref="DropDownItem"/>.</param>
        /// <param name="selected">Value that indicates whether this <see cref="DropDownItem"/> is selected.</param>
        /// <param name="disabled">Value that indicates whether this <see cref="DropDownItem"/> is disabled.</param>
        public DropDownItem(string text, string value, bool selected, bool disabled)
            : this(text, value, selected)
        {
            Disabled = disabled;
        }

        /// <summary>
        /// Gets or sets a value that indicates whether this <see cref="DropDownItem"/> is disabled.
        /// This property is typically rendered as a <c>disabled="disabled"</c> attribute in the HTML
        /// <c>&lt;option&gt;</c> element.
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// Represents the optgroup HTML element this item is wrapped into.
        /// In a select list, multiple groups with the same name are supported.
        /// They are compared with reference equality.
        /// </summary>
        public DropDownItemGroup Group { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether this <see cref="DropDownItem"/> is selected.
        /// This property is typically rendered as a <c>selected="selected"</c> attribute in the HTML
        /// <c>&lt;option&gt;</c> element.
        /// </summary>
        public bool Selected { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the display text of this <see cref="DropDownItem"/>.
        /// This property is typically rendered as the inner HTML in the HTML <c>&lt;option&gt;</c> element.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the value of this <see cref="DropDownItem"/>.
        /// This property is typically rendered as a <c>value="..."</c> attribute in the HTML
        /// <c>&lt;option&gt;</c> element.
        /// </summary>
        public string Value { get; set; }

        //
        // Summary:
        //     Gets or sets the value of the selected item.
        //
        // Returns:
        //     The ThirdValue.
        public string ThirdValue { get; set; }



    }

    /// <summary>
    /// Represents the optgroup HTML element and its attributes.
    /// In a select list, multiple groups with the same name are supported.
    /// They are compared with reference equality.
    /// </summary>
    public class DropDownItemGroup
    {
        /// <summary>
        /// Gets or sets a value that indicates whether this <see cref="DropDownItemGroup"/> is disabled.
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// Represents the value of the optgroup's label.
        /// </summary>
        public string Name { get; set; }
    }
}
