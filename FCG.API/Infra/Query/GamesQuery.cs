namespace FCG.API.Infra.Query
{
    public static class GamesQuery
    {
        public static readonly string ListAll = 
            @"  
                SELECT * 
                    FROM ""Games""
            ";  
    }
}
