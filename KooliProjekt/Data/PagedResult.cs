//kogu see asi saab k]lge baasklassi osa ja siis laiendab seda. PagedResultBase-st
//tulevad siis vist nrid 
//see Paged result siis lisab andmete osa. 

namespace KooliProjekt.Data
{
    public class PagedResult<Placeholder_Table> : PagedResultBase 
        //t on siis mingi placegholder mis viitab tabelile "Table"
    {
        public IList<Placeholder_Table> Results { get; set; } 
        //lisab juurde t[[bipoarameetri loendi results. 

        public PagedResult() 
        {
            Results = new List<Placeholder_Table>();
        }
    }
    
    
}
