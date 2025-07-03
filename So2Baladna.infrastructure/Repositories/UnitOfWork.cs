using AutoMapper;
using So2Baladna.Core.Interfaces;
using So2Baladna.Core.Services;
using So2Baladna.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IImageManagementService imageManagementService;
        public ICategoryrRepository CategoryRepository { get; }

        public IProductRepository ProductRepository { get; }

        public IPhotoRepository PhotoRepository { get; }
        public UnitOfWork(ApplicationDbContext context , IMapper mapper , IImageManagementService imageManagementService)
        {
            this.context = context;
            this.mapper = mapper;
            this.imageManagementService = imageManagementService;

            CategoryRepository = new CategoryRepository(context);
            ProductRepository = new ProductRepository(context,mapper,imageManagementService);
            PhotoRepository = new PhotoRepository(context);
        }
    }
}
