﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects.Search;

namespace Din.Service.Systems
{
    //TODO this class can maybe be removed ????
    //I still use if for refrence now
    public class TmdbSystem
    {
        private readonly TMDbClient _tmDbClient;

        public TmdbSystem()
        {
            //TODO needs key
            _tmDbClient = new TMDbClient("");
        }

        public async Task<List<SearchMovie>> SearchMovieAsync(string searchQuery)
        {
            return (await _tmDbClient.SearchMovieAsync(searchQuery)).Results;
        }

        public async Task<List<SearchTv>> SearchTvShowAsync(string searchQuery)
        {
            return (await _tmDbClient.SearchTvShowAsync(searchQuery)).Results;
        }

        public async Task<string> GetTvShowTvdbId(int id)
        {
            var result = await _tmDbClient.GetTvShowExternalIdsAsync(id);
            return result.TvdbId;
        }

        public async Task<List<SearchTvSeason>> GetTvShowSeasons(int id)
        {
            return (await _tmDbClient.GetTvShowAsync(id)).Seasons;
        }
    }
}