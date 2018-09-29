﻿namespace AtomicTorch.CBND.Automaton.Bootstrappers
{
    using AtomicTorch.CBND.CoreMod.Bootstrappers;
    using AtomicTorch.CBND.CoreMod.ClientComponents.Input;
    using AtomicTorch.CBND.Automaton.UI.Controls.Core;
    using AtomicTorch.CBND.CoreMod.UI.Controls.Core.DebugTools;
    using AtomicTorch.CBND.GameApi.Data;
    using AtomicTorch.CBND.GameApi.Scripting;

    [PrepareOrder(afterType: typeof(BootstrapperClientCoreUI))]
    public class BootstrapperClientAutomaton : BaseBootstrapper
    {
        public override void ClientInitialize()
        {
            ClientInputContext.Start("Automaton options toggle")
                              .HandleButtonDown(
                                  GameButton.ToggleDebugToolsOverlay, () => {
                                      DebugToolsOverlay.Toggle();
                                      AutomatonOverlay.Toggle();
                                  });
        }
    }
}