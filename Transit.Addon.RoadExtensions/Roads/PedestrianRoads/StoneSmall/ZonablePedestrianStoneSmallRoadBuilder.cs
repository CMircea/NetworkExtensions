﻿using Transit.Addon.RoadExtensions.Menus.Roads;
using Transit.Addon.RoadExtensions.Roads.PedestrianRoads.Common;
using Transit.Framework;
using Transit.Framework.Builders;
using Transit.Framework.Network;

namespace Transit.Addon.RoadExtensions.Roads.PedestrianRoads.StoneSmall
{
    public partial class ZonablePedestrianStoneSmallRoadBuilder : Activable, INetInfoBuilderPart, INetInfoLateBuilder
    {
        public int Order { get { return 320; } }
        public int UIOrder { get { return 40; } }

        public const string NAME = "Zonable Pedestrian Small Stone Road";
        public string BasedPrefabName { get { return NetInfos.Vanilla.ROAD_2L; } }
        public string Name { get { return NAME; } }
        public string DisplayName { get { return "[BETA] Zonable Pedestrian Small Stone Road"; } }
        public string Description { get { return "Small stone pedestrian Roads are only accessible to pedestrians and emergency vehicles"; } }
        public string ShortDescription { get { return "No Passenger Vehicles, zoneable [Traffic++ V2 required]"; } }
        public string UICategory { get { return RExExtendedMenus.ROADS_PEDESTRIANS; } }

        public string ThumbnailsPath { get { return @"Roads\PedestrianRoads\StoneSmall\thumbnails.png"; } }
        public string InfoTooltipPath { get { return @"Roads\PedestrianRoads\StoneSmall\infotooltip.png"; } }

        public NetInfoVersion SupportedVersions
        {
            get { return NetInfoVersion.Ground | NetInfoVersion.Elevated | NetInfoVersion.Bridge; }
        }

        public void BuildUp(NetInfo info, NetInfoVersion version)
        {
            ///////////////////////////
            // Set up                //
            ///////////////////////////
            info.m_createGravel = false;
            info.m_createPavement = true;
            ZPBB.SetInfo(info, version);
            ///////////////////////////
            // Texturing             //
            ///////////////////////////

            SetupTextures(info, version);

            ///////////////////////////
            // AI                    //
            ///////////////////////////
            var pedestrianVanilla = Prefabs.Find<NetInfo>(NetInfos.Vanilla.PED_PAVEMENT);
            switch (version)
            {
                case NetInfoVersion.Ground:
                    {
                        var vanillaplayerNetAI = pedestrianVanilla.GetComponent<PlayerNetAI>();
                        var playerNetAI = info.GetComponent<PlayerNetAI>();

                        if (playerNetAI != null)
                        {
                            playerNetAI.m_constructionCost = vanillaplayerNetAI.m_constructionCost * 2;
                            playerNetAI.m_maintenanceCost = vanillaplayerNetAI.m_maintenanceCost * 2;
                        }
                    }
                    break;
            }
        }

        public void LateBuildUp(NetInfo info, NetInfoVersion version)
        {
            var bollardName = "StoneBollard";
            var bollardInfo = PrefabCollection<PropInfo>.FindLoaded($"478820060.{bollardName}_Data");
            if (bollardInfo == null)
            {
                bollardInfo = PrefabCollection<PropInfo>.FindLoaded($"{bollardName}.{bollardName}_Data");
            }
            ZPBB.LateBuildUpInfo(info, version, bollardInfo);
        }
    }
}