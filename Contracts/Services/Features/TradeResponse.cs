using Contracts.Models;

namespace Contracts.Services.Features
{
    public class TradeResponse
    {   //Controllerda en son donecek ve tabloda gosterilecek olan response modeli
        public List<TableModel> Table { get; set; }
    }
}
