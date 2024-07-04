using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public interface IUiFactory
{
    UniTask CreateShop();
    UniTask CreateUiRoot();
}
