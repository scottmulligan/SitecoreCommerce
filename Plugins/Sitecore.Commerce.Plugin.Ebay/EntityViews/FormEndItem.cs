﻿
using System.Threading.Tasks;

namespace Sitecore.Commerce.Plugin.Ebay
{
    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.EntityViews;
    using Sitecore.Framework.Conditions;
    using Sitecore.Framework.Pipelines;
    using System.Collections.Generic;

    /// <summary>
    /// Defines a block which populates an EntityView for a list of Coupons with the View named Public Coupons.
    /// </summary>
    /// <seealso>
    ///     <cref>
    ///         Sitecore.Framework.Pipelines.PipelineBlock{Sitecore.Commerce.EntityViews.EntityView,
    ///         Sitecore.Commerce.EntityViews.EntityView, Sitecore.Commerce.Core.CommercePipelineExecutionContext}
    ///     </cref>
    /// </seealso>
    [PipelineDisplayName("FormEndItem")]
    public class FormEndItem : PipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
        private readonly CommerceCommander _commerceCommander;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormEndItem"/> class.
        /// </summary>
        /// <param name="commerceCommander">The <see cref="CommerceCommander"/> is a gateway object to resolving and executing other Commerce Commands and other control points.</param>
        public FormEndItem(CommerceCommander commerceCommander)
        {
            this._commerceCommander = commerceCommander;
        }

        /// <summary>Runs the Command.</summary>
        /// <param name="entityView">The argument.</param>
        /// <param name="context">The context.</param>
        /// <returns>The <see cref="EntityView"/>.</returns>
        public override Task<EntityView> Run(EntityView entityView, CommercePipelineExecutionContext context)
        {
            Condition.Requires(entityView).IsNotNull($"{this.Name}: The argument cannot be null");

            if (entityView.Name != "Ebay-FormEndItem")
            {
                return Task.FromResult(entityView);
            }

            var entityViewArgument = this._commerceCommander.Command<ViewCommander>().CurrentEntityViewArgument(context.CommerceContext);

            //var pluginPolicy = context.GetPolicy<PluginPolicy>();

            entityView.Properties.Add(
                new ViewProperty
                {
                    Name = "",
                    IsHidden = false,
                    IsReadOnly = true,
                    OriginalType = "Html",
                    UiType = "Html",
                    RawValue = "<img alt='This is the alternate' height=50 width=100 src='https://www.paypalobjects.com/webstatic/en_AR/mktg/merchant/pages/sell-on-ebay/image-ebay.png' style=''/>"
                });

            //entityView.Properties.Add(
            //    new ViewProperty
            //    {
            //        Name = "Reason",
            //        IsHidden = false,
            //        //IsReadOnly = true,
            //        IsRequired = true,
            //        RawValue = "Not Available"
            //    });

            entityView.Properties.Add(new ViewProperty
            {
                Name = "Reason",
                RawValue = string.Empty,
                Policies = new List<Policy>
                            {
                                new AvailableSelectionsPolicy
                                {
                                    List = new List<Selection>(){
                                        new Selection { Name = "NotAvailable", DisplayName = "Not Available", IsDefault = true },
                                        new Selection { Name = "Incorrect", DisplayName = "Incorrect Item", IsDefault = false },
                                        new Selection { Name = "LostOrBroken", DisplayName = "Item was Lost or Broken", IsDefault = false },
                                        new Selection { Name = "OtherListingError", DisplayName = "Item was Listed Incorrectly", IsDefault = false },
                                        new Selection { Name = "SellToHighBidder", DisplayName = "Item was Sold to a High Bidder", IsDefault = false },
                                        //new Selection { Name = "Sold", DisplayName = "Item has been Sold", IsDefault = false }
                                    }
                                }
                            },
                UiType = "Dropdown"
            });

            //entityView.Properties.Add(
            //    new ViewProperty
            //    {
            //        Name = "Description",
            //        IsHidden = false,
            //        //IsReadOnly = true,
            //        IsRequired = true,
            //        RawValue = "Sample Description",
            //        UiType = "RichText"
            //    });

            return Task.FromResult(entityView);
        }


    }

}
