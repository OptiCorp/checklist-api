//using Application.Punches.Dtos;
//using MediatR;
//using MobDeMob.Application.Common.Interfaces;
//using MobDeMob.Application.Mobilizations;
//using MobDeMob.Application.Punches;

//namespace Application.Punches.Queries.GetById;

//public class GetPunchQueryHandler : IRequestHandler<GetPunchQuery, PunchDto?>

//{
//    private readonly IPunchRepository _punchRepository;

//    private readonly IFileStorageRepository _fileStorageRepository;

//    private readonly ICacheRepository _cacheRepository;

//    public GetPunchQueryHandler(IPunchRepository punchRepository, IFileStorageRepository fileStorageRepository, ICacheRepository cachRepository)
//    {
//        _punchRepository = punchRepository;
//        _fileStorageRepository = fileStorageRepository;
//        _cacheRepository = cachRepository;
//    }

//    public async Task<PunchDto?> Handle(GetPunchQuery request, CancellationToken cancellationToken)
//    {
//        var punch = await _punchRepository.GetPunch(request.punchId, cancellationToken);
//        if (punch != null)
//        {
//            var blobUri = punch.ImageBlobUri;
//            if (blobUri != null && punch.CheckListId != null)
//            {
//                var containerSasUri = _cacheRepository.GetValue(punch.SectionId);
//                if (containerSasUri == null)
//                {
//                    var newContainerSAS = await _fileStorageRepository.GenerateContainerSAS(punch.CheckListId, cancellationToken);
//                    _cacheRepository.SetKeyValue(punch.CheckListId, newContainerSAS);
//                    var blobUriWithSas = _fileStorageRepository.ConcatBlobUriWithContainerSasTokenUri(blobUri, newContainerSAS);

//                    return punch.AsDto(blobUriWithSas);
//                }
//                else
//                {
//                    var blobUriWithSas = _fileStorageRepository.ConcatBlobUriWithContainerSasTokenUri(blobUri, containerSasUri);
//                    return punch.AsDto(blobUriWithSas);
//                }
//            }
//            return punch.AsDto(null);
//        }
//        return null;
//    }
//}
