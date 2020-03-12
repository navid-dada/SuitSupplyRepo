namespace SuitSupply.Order.Domain
{
        
    public enum AlternationType
    {
        Increscent = 0,
        Decreasement = 1 
    }
    
    /*public enum AlternationPart{
        Sleeves = 0, 
        Trousers = 0
    }

    public enum AlternationSide
    {
        Right = 0 , 
        Left = 1
    }*/

    public enum OrderState
    {
        Registered = 0 , 
        Paid =1 ,
        Finished = 2,
    }


}