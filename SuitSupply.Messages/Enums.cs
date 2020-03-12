namespace SuitSupply.Messages
{
        
    public enum AlterationType
    {
        Increscent = 0,
        Decreasement = 1 
    }
    
    public enum OrderState
    {
        Registered = 0 , 
        Paid =1 ,
        Finished = 2,
    }
    
    public enum AlterationPart{
        Sleeves = 0, 
        Trousers = 0
    }

    public enum AlterationSide
    {
        Right = 0 , 
        Left = 1
    }



}