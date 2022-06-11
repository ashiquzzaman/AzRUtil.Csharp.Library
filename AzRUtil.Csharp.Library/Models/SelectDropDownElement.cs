namespace AzRUtil.Csharp.Library.Models
{
    public class SelectDropDownElement<TId, TName,TOther>: SelectDropDownItem<TId, TName>
    {
        public TOther Other { get; set; }
    }
}
