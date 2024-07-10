namespace Journey.Communication.Responses;
public class ResponseTripsJson
{
    // Propriedade Trips, que é uma lista de objetos ResponseShortTripJson. Iniciada como uma lista vazia com "=[]" para não ser null.
    public List<ResponseShortTripJson> Trips { get; set; } = []; 
}
