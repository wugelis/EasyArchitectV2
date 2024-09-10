using System;
using System.Collections.Generic;
$if$ ($targetframeworkversion$ >= 3.5)using System.Linq;
$endif$using System.Text;
using System.Threading.Tasks;

namespace $rootnamespace$
{
	/// <summary>
    /// 身分驗證服務
    /// </summary>
	public interface $safeitemrootname$
	{
        AuthenticateResponse Authenticate(AuthenticateRequest model);
		IEnumerable<User> GetAll();
		User GetByUsername(string username);
		/// <summary>
		/// 取得目前 Scoped 下的使用者
		/// </summary>
		string IdentityUser { get; set; }
		/// <summary>
		/// 取得目前 Scoped 下的使用者 ID
		/// </summary>
		decimal? IdentityId { get; set; }
	}
}
