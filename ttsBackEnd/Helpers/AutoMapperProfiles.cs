using AutoMapper;
using ttsBackEnd.Dto;
using ttsBackEnd.Models;

namespace ttsBackEnd.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<FileForDownloadDto, FileDownload>();
            CreateMap<Song, FavoriteSong>();
            CreateMap<FavoriteSong, Song>();
        }
    }
}