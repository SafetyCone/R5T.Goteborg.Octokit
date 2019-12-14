using System;

using Octokit;


namespace R5T.Goteborg.Octokit
{
    public interface IProductHeaderValueProvider
    {
        ProductHeaderValue GetProductHeaderValue();
    }
}
