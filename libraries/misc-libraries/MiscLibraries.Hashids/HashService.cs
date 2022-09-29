namespace MiscLibraries.Hashids;

public class HashService
{
    
    private static readonly HashidsNet.Hashids _hashIds = new("salt value");

    public string Encode(Guid entityId)
    {
        var bytes = entityId.ToByteArray();
        var ints = bytes.Chunk(4).Select(bytes => BitConverter.ToInt32(bytes));
        
        return _hashIds.Encode(ints);
    }
    
    public Guid Decode(string hash)
    {
        var decoded = _hashIds.Decode(hash).Select(i => BitConverter.GetBytes(i));

        return new Guid(decoded.SelectMany(a => a).ToArray());
    }
}