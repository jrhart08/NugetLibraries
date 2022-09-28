using CommonDependencies;
using MediatR;

namespace MediatrDemo.WeatherDomain.Features.GetSupportedZipCodes;

public class GetSupportedZipCodesHandler : IRequestHandler<GetSupportedZipCodesRequest, GetSupportedZipCodesResponse>
{
    readonly MyDbContext _context;

    public GetSupportedZipCodesHandler(MyDbContext context)
    {
        _context = context;
    }

    public async Task<GetSupportedZipCodesResponse> Handle(GetSupportedZipCodesRequest request, CancellationToken cancellationToken)
    {
        var zipCodes = _context
            .SupportedAreas
            .Select(area => area.ZipCode)
            .ToList();

        return new(zipCodes);
    }
}
