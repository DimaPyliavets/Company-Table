using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompTable.Model
{
    public class TableModel
    {
        public int ID { get; set; }
        public DateTime DataOfCreating { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string WebPage { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CVSend { get; set; }
        public string Answer { get; set; }
    }
}
