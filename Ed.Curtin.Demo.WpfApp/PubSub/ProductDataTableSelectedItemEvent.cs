using Ed.Curtin.Demo.WpfApp.Models;
using Prism.Events;

namespace Ed.Curtin.Demo.WpfApp.PubSub
{
    public class ProductDataTableSelectedItemEvent: PubSubEvent<DataTableRow>
    {
    }
}
