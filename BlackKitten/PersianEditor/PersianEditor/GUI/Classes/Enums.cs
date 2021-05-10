using System;

namespace PersianEditor
{
    internal enum EditorThemes
    {
        Black,
        Silver,
        Transparent,
        Blue,
    }

    internal enum Languages
    {
        Persian,
        English
    }

    public enum ContextType
    {
        EngineContext,
        EditorContext
    };

    public enum NotifyType
    {
        Warning,
        Error,
        Log,
    }

    public enum NotifyAction
    {
        Clear,
        Add,
        ClearThenAdd,
    }

    public enum ConfirmAction
    {
        Cancel,
        Confirm,
    }

    public enum ToXnaHeader
    {
        AppMenu,
        Home,
        Animation,
        Build,
        Setting
    }

    public enum CmdToGDI
    {
        SetProperty,
        FocusOnRender,
        BindToTreeTag,
        RefreshContent,
        BindToAnimationEditor,
        RemoveAllAnimationEditorBindings,
        Save,
        Settings,
        BindToCutScene,
    }

    public enum CmdToXna
    {
        NOP,
        Exit,
        ExitShortcut,
        Minimize,
        SetPresentation,
        Explorer,
        Import,
        ImportTexture,
        ImportPAAnimatedCamera,
        NewScene,
        NewProject,
        OpenScene,
        OpenProject,
        SaveScene,
        SaveAsScene,
        SaveProject,
        SaveAsProject,
        PlayScene,
        Duplicate,
        SelectAll,
        InvertSelection,
        Delete,
        SyncGamePad,
        SwapBones,
        ShowAllBones,
        HiddenAllBones,
        ShowBone,
        HiddenBone,
        PlayNextFrame,
        PlayPreviousFrame,
        PlayAnimation,
        PauseAnimation,
        StopAnimation,
        PlayInverse,
        DefaultPose,
        ChangeAnimation,
        RemoveSwapped,
        MergeScene,
        OpenMixer,
        ApplyInherit,
        PickSourceObjectForBind,
        PickToBindObject,
        ApplyBindChanges,
        RemoveBind,
        //AttachWeapon,
        //AttachWheelToVehicle,
        AddLight,
        RemoveLight,
        AddParticleSyS,
        RemoveParticleSyS,
        LoadTextureParticleSyS,
        Shadows,
        CreateCutScene,
        PlayCutScene,
        PauseCutScene,
        StopCutScene,
        AddRemoveToCurrentCutScene,
        ShowAllBoundingBoxes,
        HideAllBoundingBoxes,
        ResetBoundingBox,
        SetMaxBoundingBox,
        ChangePostProcess,
        ShadowRes,
    }
}
