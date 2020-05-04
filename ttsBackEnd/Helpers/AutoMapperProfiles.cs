using AutoMapper;
using ttsBackEnd.Dto;
using ttsBackEnd.Models;
using ttsBackEnd.Services.YoutubeDL.Entities;
using ttsBackEnd.Services.YoutubeDL.Models;

namespace ttsBackEnd.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserForRegisterDto, User>();
            CreateMap<User, UserForRegisterDto>();
            CreateMap<FileForDownloadDto, FileDownload>();
            CreateMap<Song, FavoriteSong>();
            CreateMap<FavoriteSong, Song>();
            CreateMap<YoutubeDto, Youtube>();
        }
    }
}