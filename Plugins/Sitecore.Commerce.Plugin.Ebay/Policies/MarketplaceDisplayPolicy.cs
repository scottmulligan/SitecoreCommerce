﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MarketplaceDisplayPolicy.cs" company="Sitecore Corporation">
//   Copyright (c) Sitecore Corporation 1999-2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Commerce.Plugin.Ebay
{
    using Sitecore.Commerce.Core;

    /// <inheritdoc />
    /// <summary>
    /// Defines a policy
    /// </summary>
    /// <seealso cref="T:Sitecore.Commerce.Core.Policy" />
    public class MarketplaceDisplayPolicy : Policy
    {
        /// <summary>
        /// Public Constructor
        /// </summary>
        public MarketplaceDisplayPolicy()
        {
            DateTimeFormat = "yyyy-MMM-dd hh:mm";
        }
        /// <summary>
        /// Gets or sets the sample entity display.
        /// </summary>
        /// <value>
        /// The sample entity display.
        /// </value>
        public string DateTimeFormat { get; set; }
    }
}
