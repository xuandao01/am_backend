﻿using Demo.Webapi.BLayer.BaseBL;
using Demo.Webapi.Common.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : BaseController<Asset>
    {
        public AssetController(IBaseBL<Asset> baseBL) : base(baseBL)
        {
        }
    }
}
