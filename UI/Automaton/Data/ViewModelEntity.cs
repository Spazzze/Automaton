﻿namespace CryoFall.Automaton.UI.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AtomicTorch.CBND.CoreMod.StaticObjects;
    using AtomicTorch.CBND.CoreMod.StaticObjects.Loot;
    using AtomicTorch.CBND.CoreMod.UI.Controls.Core;
    using AtomicTorch.CBND.GameApi.Data;
    using AtomicTorch.CBND.GameApi.Data.Items;
    using AtomicTorch.CBND.GameApi.Data.World;
    using AtomicTorch.CBND.GameApi.Resources;
    using AtomicTorch.CBND.GameApi.Scripting;
    using AtomicTorch.GameEngine.Common.Client.MonoGame.UI;

    public class ViewModelEntity : BaseViewModel
    {
        private static readonly IReadOnlyCollection<Type> IconBlackList = new List<Type>() {
            typeof(ObjectGroundItemsContainer),
            typeof(ObjectCorpse),
        }.AsReadOnly();

        private ITextureResource iconResource = null;

        private TextureBrush icon;

        private bool isEnabled;

        public readonly IProtoEntity Entity;

        public ViewModelEntity(IProtoEntity entity, bool defaultIsEnabled = false)
        {
            Entity = entity;
            Name = entity.Name;
            Id = entity.Id;
            isEnabled = defaultIsEnabled;
        }

        public string Name { get; }

        public string Id { get; }

        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                if (value == IsEnabled)
                {
                    return;
                }

                isEnabled = value;
                NotifyThisPropertyChanged();
                IsEnabledChanged?.Invoke();
            }
        }

        public event Action IsEnabledChanged;

        public TextureBrush Icon
        {
            get
            {
                if (icon == null)
                {
                    if (!IconBlackList.Contains(Entity.GetType()))
                    {
                        switch (Entity)
                        {
                            case IProtoStaticWorldObject staticWorldObject:
                                iconResource = staticWorldObject.Icon ?? staticWorldObject.DefaultTexture;
                                break;
                            case IProtoItem protoItem:
                                iconResource = protoItem.Icon;
                                break;
                        }
                    }
                    if (iconResource == null)
                    {
                        // Default icon.
                        iconResource = new TextureResource("Content/Textures/StaticObjects/ObjectUnknown.png");
                    }
                    icon = Api.Client.UI.GetTextureBrush(iconResource);
                }
                return icon;
            }
        }
    }
}