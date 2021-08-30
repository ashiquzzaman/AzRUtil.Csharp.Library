using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;

namespace AzRUtil.Csharp.Library.Extensions
{
    public static class ClaimExtension
    {


        public static IEnumerable<Claim> GetClaims(this IIdentity user)
        {
            var claims = ((ClaimsIdentity)user)?.Claims;
            return claims;
        }
        public static IEnumerable<Claim> GetClaims(this ClaimsPrincipal user)
        {
            IEnumerable<Claim> userClaimes;
            if (user is { Claims: { } })
            {
                userClaimes = user.Claims;
            }
            else
            {
                if (user.Claims == null && user.Identity != null)
                {
                    userClaimes = ((ClaimsIdentity)user.Identity).Claims;
                }
                else
                {
                    return null;
                }
            }

            return userClaimes;
        }
        public static IEnumerable<Claim> GetClaims(this IPrincipal user)
        {
            IEnumerable<Claim> userClaims;
            ClaimsIdentity claimsIdentity;
            if (user != null)
            {
                claimsIdentity = (ClaimsIdentity)user;
            }
            else
            {
                if (user.Identity != null)
                {
                    claimsIdentity = ((ClaimsIdentity)user.Identity);
                }
                else
                {
                    return null;
                }
            }
            if (claimsIdentity == null)
            {
                return null;
            }
            else
            {
                userClaims = claimsIdentity.Claims;
            }
            return userClaims;
        }







        public static T GetUserId<T>(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var loggedInUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(loggedInUserId, typeof(T));
            }
            else if (typeof(T) == typeof(int) || typeof(T) == typeof(long))
            {
                return loggedInUserId != null ? (T)Convert.ChangeType(loggedInUserId, typeof(T)) : (T)Convert.ChangeType(0, typeof(T));
            }
            else
            {
                return default(T);
            }
        }

        public static string GetLoggedInUserName(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue(ClaimTypes.Name);
        }
        public static string GetLoggedInUserRole(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue(ClaimTypes.Role);
        }
        public static string GetLoggedInUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue(ClaimTypes.Email);
        }

        private static string FindFirstValue(this ClaimsPrincipal principal, string claimType)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            var claim = principal.FindFirst(claimType);
            return claim?.Value;
        }
        private static T FindFirstValue<T>(this ClaimsPrincipal principal, string claimType)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            var claim = principal.FindFirst(claimType);
            var val = claim?.Value;
            if (string.IsNullOrEmpty(val))
            {
                return default(T);

            }
            else if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(val, typeof(T));
            }
            else if (typeof(T) == typeof(int) || typeof(T) == typeof(long))
            {
                return (T)Convert.ChangeType(val, typeof(T));
            }
            else if (typeof(T) == typeof(bool))
            {
                return (T)Convert.ChangeType(val, typeof(T));
            }
            else if (typeof(T) == typeof(DateTime))
            {
                return (T)Convert.ChangeType(val, typeof(T));
            }
            else if (typeof(T) == typeof(Enum))
            {
                return (T)Convert.ChangeType(val, typeof(int));
            }
            else
            {
                return default(T);
            }
        }
    }
}
