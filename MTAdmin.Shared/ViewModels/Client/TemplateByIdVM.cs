using System.Collections.Generic;

namespace MTAdmin.Shared.ViewModels.Client
{
    public class TemplateByIdVM : Template
    {
        public IReadOnlyList<IdNameVM<int>> Tags { get; set; }
    }
}
