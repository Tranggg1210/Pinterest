using AutoMapper;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using PixelPalette.Data;
using PixelPalette.Entities;
using PixelPalette.Helpers;
using PixelPalette.Interfaces;
using PixelPalette.Models;
using System.Collections.ObjectModel;

namespace PixelPalette.Repositories
{
    public class CollectionRepository : ICollectionRepository
    {
        private readonly PixelPaletteContext _context;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly ITool _tools;

        public CollectionRepository(PixelPaletteContext context, IMapper mapper, IPhotoService photoService, ITool tools)
        {
            _context = context;
            _mapper = mapper;
            _photoService = photoService;
            _tools = tools;
        }

        public async Task<CollectionModel> AddCollectionAsync(int userId, CollectCreateParams entryParams)
        {
            var colection = new Entities.Collection();
            _tools.Duplicate(entryParams, ref colection);
            colection.UserId = userId;
            _context.Collections.Add(colection);
            await _context.SaveChangesAsync();
            return _mapper.Map<CollectionModel>(colection);
        }

        public async Task<bool> DeleteCollectionAsync(int id)
        {
            var deleteCollection = _context.Collections!.SingleOrDefault(c => c.Id == id);
            if (deleteCollection != null)
            {
                _context.Collections!.Remove(deleteCollection);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<CollectionModel>> GetAllCollectionAsync()
        {
            var collections = await _context.Collections!.ToListAsync();
            return _mapper.Map<IEnumerable<CollectionModel>>(collections);
        }

        public async Task<CollectionModel> GetCollectionByIdAsync(int id)
        {
            var collection = await _context.Collections!.FindAsync(id);
            return _mapper.Map<CollectionModel>(collection);
        }

        public async Task<CollectionModel> UpdateCollectionAsync(int id, CollectUpdateParams entryParams)
        {
            var updateCollection = await _context.Collections!.FindAsync(id);
            if (updateCollection != null)
            {
                _tools.Duplicate(entryParams, ref updateCollection);
                _context.Collections!.Update(updateCollection);
                await _context.SaveChangesAsync();
                return _mapper.Map<CollectionModel>(updateCollection);
            }
            return null!;
        }

        public async Task<string> EditBackgroundAsync(int id, IFormFile file)
        {
            var collection = await _context.Collections!.FindAsync(id);
            if (collection != null)
            {
                var addResult = await _photoService.AddPhotoAsync(file);
                if (addResult.Error != null) return string.Empty;

                if (collection.BackgroundId != null)
                {
                    var deleteResult = await _photoService.DeletePhotoAsync(collection.BackgroundId);
                    if (deleteResult.Error != null || deleteResult.Result == "not found") return string.Empty;
                }
                var Url = addResult.SecureUrl.AbsoluteUri;
                collection.BackgroundUrl = Url;
                collection.BackgroundId = addResult.PublicId;
                _context.Collections!.Update(collection);
                await _context.SaveChangesAsync();
                return Url;
            }
            return string.Empty;
        }

        public async Task<IEnumerable<CollectionModel>> GetCollectionByUserIdAsync(int userId)
        {
            var collections = await _context.Collections
                .Where(p => p.UserId == userId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CollectionModel>>(collections);
        }

        public async Task<IEnumerable<CollectionModel>> GetCollectionByPostIdAsync(int postId)
        {
            var collections = await _context.Ownerships!
                .Where(o => o.PostId == postId)
                .Join(_context.Collections, o => o.CollectionId, c => c.Id, (o, c) => c)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CollectionModel>>(collections);
        }

        public async Task<int> CountPostByCollectionId(int id)
        {
            return await _context.Ownerships!
                .Where(o => o.CollectionId == id)
                .CountAsync();
        }

        public async Task<bool> CheckOwnCollectionAsync(int postId, int userId)
        {
            return await _context.Ownerships
                .Join(_context.Collections, o => o.CollectionId, c => c.Id, (o, c) => new { o, c })
                .AnyAsync(oc => oc.c.UserId == userId && oc.c.IsDefault && oc.o.PostId == postId);
        }
    }
}
