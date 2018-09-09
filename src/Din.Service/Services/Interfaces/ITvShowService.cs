﻿using System.Threading.Tasks;
using Din.Service.DTO;
using Din.Service.DTO.Content;
using TMDbLib.Objects.Search;

namespace Din.Service.Services.Interfaces
{
    public interface ITvShowService
    {
        /// <summary>
        /// Search Tv Shows with a specific query.
        /// </summary>
        /// <param name="query">The Tv Show title or a part of it.</param>
        /// <returns>ViewModel containing collections of existing movies and query results.</returns>
        Task<TvShowDTO> SearchTvShowAsync(string query);

        /// <summary>
        /// Adds TvShow to the system.
        /// </summary>
        /// <param name="tvShow">The TvShow object that needs to be added.</param>
        /// <param name="id">The account id of the current session.</param>
        /// <returns>The status result.</returns>
        Task<ResultDTO> AddTvShowAsync(SearchTv tvShow, int id);
    }
}