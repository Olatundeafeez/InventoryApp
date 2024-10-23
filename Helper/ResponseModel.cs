namespace InventoryAPI.Helper
{
    public class ResponseModel<T>
    {
        public bool IsSucessful { get; set; }
        public string Message { get; set; }
        public int ErrorCode { get; set; }
        public T Result { get; set; }

        //response message for a successfull operation
        public ResponseModel<T> Successful(T result)
        {
            var res = new ResponseModel<T>
            {
                    IsSucessful = true,
                    Message = "Done Successfully ",
                    ErrorCode = 200,
                    Result = result
            };
            return res;
        }
            // failed result method

        public ResponseModel<T> FailedResult(T result) 
        {
            var res = new ResponseModel<T>()
            {
                IsSucessful = false,
                Message = "Not Successfully ",
                ErrorCode = 200,
                Result = result
            };
            return res;
        } 

    }
    
}
