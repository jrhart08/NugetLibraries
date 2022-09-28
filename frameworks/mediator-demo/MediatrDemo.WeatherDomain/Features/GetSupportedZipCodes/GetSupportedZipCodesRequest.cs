using MediatR;

namespace MediatrDemo.WeatherDomain.Features.GetSupportedZipCodes;

public readonly record struct GetSupportedZipCodesRequest : IRequest<GetSupportedZipCodesResponse>;

public readonly record struct GetSupportedZipCodesResponse(List<string> ZipCodes);
