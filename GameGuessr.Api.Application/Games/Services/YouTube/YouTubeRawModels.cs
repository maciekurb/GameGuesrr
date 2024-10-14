// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Param
    {
        public string key { get; set; }
        public string value { get; set; }
    }

    public class ServiceTrackingParam
    {
        public string service { get; set; }
        public List<Param> @params { get; set; }
    }

    public class MainAppWebResponseContext
    {
        public bool loggedOut { get; set; }
    }

    public class YtConfigData
    {
        public string visitorData { get; set; }
        public int rootVisualElementType { get; set; }
    }

    public class WebResponseContextExtensionData
    {
        public YtConfigData ytConfigData { get; set; }
        public bool hasDecorated { get; set; }
    }

    public class ResponseContext
    {
        public List<ServiceTrackingParam> serviceTrackingParams { get; set; }
        public MainAppWebResponseContext mainAppWebResponseContext { get; set; }
        public WebResponseContextExtensionData webResponseContextExtensionData { get; set; }
    }

    public class Thumbnail2
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public List<Thumbnail> thumbnails { get; set; }
    }

    public class Thumbnail
    {
        public List<Thumbnail> thumbnails { get; set; }
    }

    public class Run
    {
        public string text { get; set; }
        public NavigationEndpoint navigationEndpoint { get; set; }
        public bool bold { get; set; }
    }

    public class AccessibilityData
    {
        public string label { get; set; }
        public AccessibilityData accessibilityData { get; set; }
    }

    public class Accessibility
    {
        public AccessibilityData accessibilityData { get; set; }
        public string label { get; set; }
    }

    public class Title
    {
        public List<Run> runs { get; set; }
        public Accessibility accessibility { get; set; }
        public string simpleText { get; set; }
    }

    public class WebCommandMetadata
    {
        public string url { get; set; }
        public string webPageType { get; set; }
        public int rootVe { get; set; }
        public string apiUrl { get; set; }
        public bool sendPost { get; set; }
    }

    public class CommandMetadata
    {
        public WebCommandMetadata webCommandMetadata { get; set; }
    }

    public class BrowseEndpoint
    {
        public string browseId { get; set; }
        public string canonicalBaseUrl { get; set; }
    }

    public class NavigationEndpoint
    {
        public string clickTrackingParams { get; set; }
        public CommandMetadata commandMetadata { get; set; }
        public BrowseEndpoint browseEndpoint { get; set; }
        public WatchEndpoint watchEndpoint { get; set; }
        public SearchEndpoint searchEndpoint { get; set; }
        public UrlEndpoint urlEndpoint { get; set; }
        public SignInEndpoint signInEndpoint { get; set; }
    }

    public class LongBylineText
    {
        public List<Run> runs { get; set; }
    }

    public class PublishedTimeText
    {
        public string simpleText { get; set; }
    }

    public class LengthText
    {
        public Accessibility accessibility { get; set; }
        public string simpleText { get; set; }
    }

    public class ViewCountText
    {
        public string simpleText { get; set; }
        public List<Run> runs { get; set; }
    }

    public class CommonConfig
    {
        public string url { get; set; }
    }

    public class Html5PlaybackOnesieConfig
    {
        public CommonConfig commonConfig { get; set; }
    }

    public class WatchEndpointSupportedOnesieConfig
    {
        public Html5PlaybackOnesieConfig html5PlaybackOnesieConfig { get; set; }
    }

    public class WatchEndpoint
    {
        public string videoId { get; set; }
        public string @params { get; set; }
        public WatchEndpointSupportedOnesieConfig watchEndpointSupportedOnesieConfig { get; set; }
        public string playlistId { get; set; }
        public LoggingContext loggingContext { get; set; }
    }

    public class OwnerText
    {
        public List<Run> runs { get; set; }
    }

    public class ShortBylineText
    {
        public List<Run> runs { get; set; }
    }

    public class ShortViewCountText
    {
        public Accessibility accessibility { get; set; }
        public string simpleText { get; set; }
        public List<Run> runs { get; set; }
    }

    public class Text
    {
        public List<Run> runs { get; set; }
        public Accessibility accessibility { get; set; }
        public string simpleText { get; set; }
    }

    public class Icon
    {
        public string iconType { get; set; }
    }

    public class CreatePlaylistServiceEndpoint
    {
        public List<string> videoIds { get; set; }
        public string @params { get; set; }
    }

    public class OnCreateListCommand
    {
        public string clickTrackingParams { get; set; }
        public CommandMetadata commandMetadata { get; set; }
        public CreatePlaylistServiceEndpoint createPlaylistServiceEndpoint { get; set; }
    }

    public class AddToPlaylistCommand
    {
        public bool openMiniplayer { get; set; }
        public string videoId { get; set; }
        public string listType { get; set; }
        public OnCreateListCommand onCreateListCommand { get; set; }
        public List<string> videoIds { get; set; }
    }

    public class Action
    {
        public string clickTrackingParams { get; set; }
        public AddToPlaylistCommand addToPlaylistCommand { get; set; }
        public string addedVideoId { get; set; }
        public string action { get; set; }
        public string removedVideoId { get; set; }
        public SelectLanguageCommand selectLanguageCommand { get; set; }
        public OpenPopupAction openPopupAction { get; set; }
        public SignalAction signalAction { get; set; }
    }

    public class SignalServiceEndpoint
    {
        public string signal { get; set; }
        public List<Action> actions { get; set; }
    }

    public class ServiceEndpoint
    {
        public string clickTrackingParams { get; set; }
        public CommandMetadata commandMetadata { get; set; }
        public SignalServiceEndpoint signalServiceEndpoint { get; set; }
    }

    public class MenuServiceItemRenderer
    {
        public Text text { get; set; }
        public Icon icon { get; set; }
        public ServiceEndpoint serviceEndpoint { get; set; }
        public string trackingParams { get; set; }
    }

    public class Item
    {
        public MenuServiceItemRenderer menuServiceItemRenderer { get; set; }
        public VideoRenderer videoRenderer { get; set; }
        public List<Run> runs { get; set; }
        public CompactLinkRenderer compactLinkRenderer { get; set; }
    }

    public class MenuRenderer
    {
        public List<Item> items { get; set; }
        public string trackingParams { get; set; }
        public Accessibility accessibility { get; set; }
        public MultiPageMenuRenderer multiPageMenuRenderer { get; set; }
    }

    public class Menu
    {
        public MenuRenderer menuRenderer { get; set; }
    }

    public class ChannelThumbnailWithLinkRenderer
    {
        public Thumbnail thumbnail { get; set; }
        public NavigationEndpoint navigationEndpoint { get; set; }
        public Accessibility accessibility { get; set; }
    }

    public class ChannelThumbnailSupportedRenderers
    {
        public ChannelThumbnailWithLinkRenderer channelThumbnailWithLinkRenderer { get; set; }
    }

    public class ThumbnailOverlayTimeStatusRenderer
    {
        public Text text { get; set; }
        public string style { get; set; }
    }

    public class UntoggledIcon
    {
        public string iconType { get; set; }
    }

    public class ToggledIcon
    {
        public string iconType { get; set; }
    }

    public class PlaylistEditEndpoint
    {
        public string playlistId { get; set; }
        public List<Action> actions { get; set; }
    }

    public class UntoggledServiceEndpoint
    {
        public string clickTrackingParams { get; set; }
        public CommandMetadata commandMetadata { get; set; }
        public PlaylistEditEndpoint playlistEditEndpoint { get; set; }
        public SignalServiceEndpoint signalServiceEndpoint { get; set; }
    }

    public class ToggledServiceEndpoint
    {
        public string clickTrackingParams { get; set; }
        public CommandMetadata commandMetadata { get; set; }
        public PlaylistEditEndpoint playlistEditEndpoint { get; set; }
    }

    public class UntoggledAccessibility
    {
        public AccessibilityData accessibilityData { get; set; }
    }

    public class ToggledAccessibility
    {
        public AccessibilityData accessibilityData { get; set; }
    }

    public class ThumbnailOverlayToggleButtonRenderer
    {
        public bool isToggled { get; set; }
        public UntoggledIcon untoggledIcon { get; set; }
        public ToggledIcon toggledIcon { get; set; }
        public string untoggledTooltip { get; set; }
        public string toggledTooltip { get; set; }
        public UntoggledServiceEndpoint untoggledServiceEndpoint { get; set; }
        public ToggledServiceEndpoint toggledServiceEndpoint { get; set; }
        public UntoggledAccessibility untoggledAccessibility { get; set; }
        public ToggledAccessibility toggledAccessibility { get; set; }
        public string trackingParams { get; set; }
    }

    public class ThumbnailOverlayNowPlayingRenderer
    {
        public Text text { get; set; }
    }

    public class ThumbnailOverlay
    {
        public ThumbnailOverlayTimeStatusRenderer thumbnailOverlayTimeStatusRenderer { get; set; }
        public ThumbnailOverlayToggleButtonRenderer thumbnailOverlayToggleButtonRenderer { get; set; }
        public ThumbnailOverlayNowPlayingRenderer thumbnailOverlayNowPlayingRenderer { get; set; }
        public ThumbnailOverlaySidePanelRenderer thumbnailOverlaySidePanelRenderer { get; set; }
        public ThumbnailOverlayHoverTextRenderer thumbnailOverlayHoverTextRenderer { get; set; }
    }

    public class SnippetText
    {
        public List<Run> runs { get; set; }
    }

    public class SnippetHoverText
    {
        public List<Run> runs { get; set; }
    }

    public class DetailedMetadataSnippet
    {
        public SnippetText snippetText { get; set; }
        public SnippetHoverText snippetHoverText { get; set; }
        public bool maxOneLine { get; set; }
    }

    public class VideoRenderer
    {
        public string videoId { get; set; }
        public Thumbnail thumbnail { get; set; }
        public Title title { get; set; }
        public LongBylineText longBylineText { get; set; }
        public PublishedTimeText publishedTimeText { get; set; }
        public LengthText lengthText { get; set; }
        public ViewCountText viewCountText { get; set; }
        public NavigationEndpoint navigationEndpoint { get; set; }
        public OwnerText ownerText { get; set; }
        public ShortBylineText shortBylineText { get; set; }
        public string trackingParams { get; set; }
        public bool showActionMenu { get; set; }
        public ShortViewCountText shortViewCountText { get; set; }
        public Menu menu { get; set; }
        public ChannelThumbnailSupportedRenderers channelThumbnailSupportedRenderers { get; set; }
        public List<ThumbnailOverlay> thumbnailOverlays { get; set; }
        public List<DetailedMetadataSnippet> detailedMetadataSnippets { get; set; }
        public List<Badge> badges { get; set; }
        public List<OwnerBadge> ownerBadges { get; set; }
    }

    public class VssLoggingContext
    {
        public string serializedContextData { get; set; }
    }

    public class LoggingContext
    {
        public VssLoggingContext vssLoggingContext { get; set; }
    }

    public class ViewPlaylistText
    {
        public List<Run> runs { get; set; }
    }

    public class ChildVideoRenderer
    {
        public Title title { get; set; }
        public NavigationEndpoint navigationEndpoint { get; set; }
        public LengthText lengthText { get; set; }
        public string videoId { get; set; }
    }

    public class Video
    {
        public ChildVideoRenderer childVideoRenderer { get; set; }
    }

    public class VideoCountText
    {
        public List<Run> runs { get; set; }
    }

    public class ThumbnailText
    {
        public List<Run> runs { get; set; }
    }

    public class PlaylistVideoThumbnailRenderer
    {
        public Thumbnail thumbnail { get; set; }
    }

    public class ThumbnailRenderer
    {
        public PlaylistVideoThumbnailRenderer playlistVideoThumbnailRenderer { get; set; }
    }

    public class ThumbnailOverlaySidePanelRenderer
    {
        public Text text { get; set; }
        public Icon icon { get; set; }
    }

    public class ThumbnailOverlayHoverTextRenderer
    {
        public Text text { get; set; }
        public Icon icon { get; set; }
    }

    public class PlaylistRenderer
    {
        public string playlistId { get; set; }
        public Title title { get; set; }
        public List<Thumbnail> thumbnails { get; set; }
        public string videoCount { get; set; }
        public NavigationEndpoint navigationEndpoint { get; set; }
        public ViewPlaylistText viewPlaylistText { get; set; }
        public ShortBylineText shortBylineText { get; set; }
        public List<Video> videos { get; set; }
        public VideoCountText videoCountText { get; set; }
        public string trackingParams { get; set; }
        public ThumbnailText thumbnailText { get; set; }
        public LongBylineText longBylineText { get; set; }
        public ThumbnailRenderer thumbnailRenderer { get; set; }
        public List<ThumbnailOverlay> thumbnailOverlays { get; set; }
    }

    public class MetadataBadgeRenderer
    {
        public string style { get; set; }
        public string label { get; set; }
        public string trackingParams { get; set; }
        public Icon icon { get; set; }
        public string tooltip { get; set; }
        public AccessibilityData accessibilityData { get; set; }
    }

    public class Badge
    {
        public MetadataBadgeRenderer metadataBadgeRenderer { get; set; }
    }

    public class OwnerBadge
    {
        public MetadataBadgeRenderer metadataBadgeRenderer { get; set; }
    }

    public class CollapsedStateButtonText
    {
        public List<Run> runs { get; set; }
        public Accessibility accessibility { get; set; }
    }

    public class VerticalListRenderer
    {
        public List<Item> items { get; set; }
        public int collapsedItemCount { get; set; }
        public CollapsedStateButtonText collapsedStateButtonText { get; set; }
        public string trackingParams { get; set; }
    }

    public class Content
    {
        public VerticalListRenderer verticalListRenderer { get; set; }
    }

    public class ShelfRenderer
    {
        public Title title { get; set; }
        public Content content { get; set; }
        public string trackingParams { get; set; }
    }

    public class Content2
    {
        public VideoRenderer videoRenderer { get; set; }
        public PlaylistRenderer playlistRenderer { get; set; }
        public ShelfRenderer shelfRenderer { get; set; }
        public ItemSectionRenderer itemSectionRenderer { get; set; }
        public ContinuationItemRenderer continuationItemRenderer { get; set; }
        public TwoColumnSearchResultsRenderer twoColumnSearchResultsRenderer { get; set; }
    }

    public class ItemSectionRenderer
    {
        public List<Content2> contents { get; set; }
        public string trackingParams { get; set; }
    }

    public class ContinuationCommand
    {
        public string token { get; set; }
        public string request { get; set; }
    }

    public class ContinuationEndpoint
    {
        public string clickTrackingParams { get; set; }
        public CommandMetadata commandMetadata { get; set; }
        public ContinuationCommand continuationCommand { get; set; }
    }

    public class ContinuationItemRenderer
    {
        public string trigger { get; set; }
        public ContinuationEndpoint continuationEndpoint { get; set; }
    }

    public class Label
    {
        public string simpleText { get; set; }
        public List<Run> runs { get; set; }
    }

    public class SearchEndpoint
    {
        public string query { get; set; }
        public string @params { get; set; }
        public string clickTrackingParams { get; set; }
        public CommandMetadata commandMetadata { get; set; }
        public SearchEndpoint searchEndpoint { get; set; }
    }

    public class SearchFilterRenderer
    {
        public Label label { get; set; }
        public NavigationEndpoint navigationEndpoint { get; set; }
        public string tooltip { get; set; }
        public string trackingParams { get; set; }
        public string status { get; set; }
    }

    public class Filter
    {
        public SearchFilterRenderer searchFilterRenderer { get; set; }
    }

    public class SearchFilterGroupRenderer
    {
        public Title title { get; set; }
        public List<Filter> filters { get; set; }
        public string trackingParams { get; set; }
    }

    public class Group
    {
        public SearchFilterGroupRenderer searchFilterGroupRenderer { get; set; }
    }

    public class Style
    {
        public string styleType { get; set; }
    }

    public class DefaultIcon
    {
        public string iconType { get; set; }
    }

    public class DefaultText
    {
        public List<Run> runs { get; set; }
    }

    public class ToggledStyle
    {
        public string styleType { get; set; }
    }

    public class ToggleButtonRenderer
    {
        public Style style { get; set; }
        public bool isToggled { get; set; }
        public bool isDisabled { get; set; }
        public DefaultIcon defaultIcon { get; set; }
        public DefaultText defaultText { get; set; }
        public Accessibility accessibility { get; set; }
        public string trackingParams { get; set; }
        public string defaultTooltip { get; set; }
        public string toggledTooltip { get; set; }
        public ToggledStyle toggledStyle { get; set; }
        public AccessibilityData accessibilityData { get; set; }
    }

    public class Button
    {
        public ToggleButtonRenderer toggleButtonRenderer { get; set; }
    }

    public class SearchSubMenuRenderer
    {
        public Title title { get; set; }
        public List<Group> groups { get; set; }
        public string trackingParams { get; set; }
        public Button button { get; set; }
    }

    public class SubMenu
    {
        public SearchSubMenuRenderer searchSubMenuRenderer { get; set; }
    }

    public class SectionListRenderer
    {
        public List<Content2> contents { get; set; }
        public string trackingParams { get; set; }
        public SubMenu subMenu { get; set; }
        public bool hideBottomSeparator { get; set; }
        public string targetId { get; set; }
    }

    public class PrimaryContents
    {
        public SectionListRenderer sectionListRenderer { get; set; }
    }

    public class TwoColumnSearchResultsRenderer
    {
        public PrimaryContents primaryContents { get; set; }
    }

    public class IconImage
    {
        public string iconType { get; set; }
    }

    public class TooltipText
    {
        public List<Run> runs { get; set; }
    }

    public class Endpoint
    {
        public string clickTrackingParams { get; set; }
        public CommandMetadata commandMetadata { get; set; }
        public BrowseEndpoint browseEndpoint { get; set; }
    }

    public class TopbarLogoRenderer
    {
        public IconImage iconImage { get; set; }
        public TooltipText tooltipText { get; set; }
        public Endpoint endpoint { get; set; }
        public string trackingParams { get; set; }
        public string overrideEntityKey { get; set; }
    }

    public class Logo
    {
        public TopbarLogoRenderer topbarLogoRenderer { get; set; }
    }

    public class PlaceholderText
    {
        public List<Run> runs { get; set; }
    }

    public class WebSearchboxConfig
    {
        public string requestLanguage { get; set; }
        public string requestDomain { get; set; }
        public bool hasOnscreenKeyboard { get; set; }
        public bool focusSearchbox { get; set; }
    }

    public class Config
    {
        public WebSearchboxConfig webSearchboxConfig { get; set; }
    }

    public class ButtonRenderer
    {
        public string style { get; set; }
        public string size { get; set; }
        public bool isDisabled { get; set; }
        public Icon icon { get; set; }
        public string trackingParams { get; set; }
        public AccessibilityData accessibilityData { get; set; }
        public Text text { get; set; }
        public Accessibility accessibility { get; set; }
        public Command command { get; set; }
        public string tooltip { get; set; }
        public string iconPosition { get; set; }
        public NavigationEndpoint navigationEndpoint { get; set; }
        public string targetId { get; set; }
        public ServiceEndpoint serviceEndpoint { get; set; }
    }

    public class ClearButton
    {
        public ButtonRenderer buttonRenderer { get; set; }
    }

    public class FusionSearchboxRenderer
    {
        public Icon icon { get; set; }
        public PlaceholderText placeholderText { get; set; }
        public Config config { get; set; }
        public string trackingParams { get; set; }
        public SearchEndpoint searchEndpoint { get; set; }
        public ClearButton clearButton { get; set; }
    }

    public class Searchbox
    {
        public FusionSearchboxRenderer fusionSearchboxRenderer { get; set; }
    }

    public class InterstitialLogoAside
    {
        public List<Run> runs { get; set; }
    }

    public class LanguagePickerButton
    {
        public ButtonRenderer buttonRenderer { get; set; }
    }

    public class InterstitialTitle
    {
        public List<Run> runs { get; set; }
    }

    public class UrlEndpoint
    {
        public string url { get; set; }
        public string target { get; set; }
    }

    public class InterstitialMessage
    {
        public List<Run> runs { get; set; }
    }

    public class Command
    {
        public string clickTrackingParams { get; set; }
        public CommandMetadata commandMetadata { get; set; }
        public UrlEndpoint urlEndpoint { get; set; }
        public SaveConsentAction saveConsentAction { get; set; }
        public SignInEndpoint signInEndpoint { get; set; }
        public SignalServiceEndpoint signalServiceEndpoint { get; set; }
    }

    public class CustomizeButton
    {
        public ButtonRenderer buttonRenderer { get; set; }
    }

    public class SaveConsentAction
    {
        public string consentSaveUrl { get; set; }
        public string consentCookie { get; set; }
        public string visitorCookie { get; set; }
        public string serializedVisitorData { get; set; }
        public string socsCookie { get; set; }
        public bool enableEom { get; set; }
        public string savePreferenceUrl { get; set; }
    }

    public class AgreeButton
    {
        public ButtonRenderer buttonRenderer { get; set; }
    }

    public class PrivacyLink
    {
        public List<Run> runs { get; set; }
    }

    public class TermsLink
    {
        public List<Run> runs { get; set; }
    }

    public class SignInEndpoint
    {
        public bool hack { get; set; }
        public string gaeParam { get; set; }
        public string idamTag { get; set; }
    }

    public class SignInButton
    {
        public ButtonRenderer buttonRenderer { get; set; }
    }

    public class Begin
    {
        public List<Run> runs { get; set; }
    }

    public class EssentialCookieMsg
    {
        public Begin begin { get; set; }
        public List<Item> items { get; set; }
    }

    public class NonEssentialCookieMsg
    {
        public Begin begin { get; set; }
        public List<Item> items { get; set; }
    }

    public class Personalization
    {
        public List<Run> runs { get; set; }
    }

    public class CustomizationOption
    {
        public List<Run> runs { get; set; }
    }

    public class V21Message
    {
        public EssentialCookieMsg essentialCookieMsg { get; set; }
        public NonEssentialCookieMsg nonEssentialCookieMsg { get; set; }
        public Personalization personalization { get; set; }
        public CustomizationOption customizationOption { get; set; }
    }

    public class SelectLanguageCommand
    {
        public string hl { get; set; }
    }

    public class OnSelectCommand
    {
        public string clickTrackingParams { get; set; }
        public CommandMetadata commandMetadata { get; set; }
        public SignalServiceEndpoint signalServiceEndpoint { get; set; }
    }

    public class DropdownItemRenderer
    {
        public Label label { get; set; }
        public bool isSelected { get; set; }
        public string stringValue { get; set; }
        public OnSelectCommand onSelectCommand { get; set; }
    }

    public class Entry
    {
        public DropdownItemRenderer dropdownItemRenderer { get; set; }
    }

    public class DropdownRenderer
    {
        public List<Entry> entries { get; set; }
        public Accessibility accessibility { get; set; }
    }

    public class LanguageList
    {
        public DropdownRenderer dropdownRenderer { get; set; }
    }

    public class ReadMoreButton
    {
        public ButtonRenderer buttonRenderer { get; set; }
    }

    public class LoadingMessage
    {
        public List<Run> runs { get; set; }
    }

    public class ErrorMessage
    {
        public List<Run> runs { get; set; }
    }

    public class ConsentBumpV2Renderer
    {
        public InterstitialLogoAside interstitialLogoAside { get; set; }
        public LanguagePickerButton languagePickerButton { get; set; }
        public InterstitialTitle interstitialTitle { get; set; }
        public InterstitialMessage interstitialMessage { get; set; }
        public CustomizeButton customizeButton { get; set; }
        public AgreeButton agreeButton { get; set; }
        public PrivacyLink privacyLink { get; set; }
        public TermsLink termsLink { get; set; }
        public string trackingParams { get; set; }
        public SignInButton signInButton { get; set; }
        public V21Message v21Message { get; set; }
        public LanguageList languageList { get; set; }
        public ReadMoreButton readMoreButton { get; set; }
        public LoadingMessage loadingMessage { get; set; }
        public ErrorMessage errorMessage { get; set; }
    }

    public class Interstitial
    {
        public ConsentBumpV2Renderer consentBumpV2Renderer { get; set; }
    }

    public class CompactLinkRenderer
    {
        public Icon icon { get; set; }
        public Title title { get; set; }
        public NavigationEndpoint navigationEndpoint { get; set; }
        public string trackingParams { get; set; }
    }

    public class MultiPageMenuSectionRenderer
    {
        public List<Item> items { get; set; }
        public string trackingParams { get; set; }
    }

    public class Section
    {
        public MultiPageMenuSectionRenderer multiPageMenuSectionRenderer { get; set; }
        public HotkeyDialogSectionRenderer hotkeyDialogSectionRenderer { get; set; }
    }

    public class MultiPageMenuRenderer
    {
        public List<Section> sections { get; set; }
        public string trackingParams { get; set; }
        public string style { get; set; }
        public bool showLoadingSpinner { get; set; }
    }

    public class Popup
    {
        public MultiPageMenuRenderer multiPageMenuRenderer { get; set; }
        public VoiceSearchDialogRenderer voiceSearchDialogRenderer { get; set; }
    }

    public class OpenPopupAction
    {
        public Popup popup { get; set; }
        public string popupType { get; set; }
        public bool beReused { get; set; }
    }

    public class MenuRequest
    {
        public string clickTrackingParams { get; set; }
        public CommandMetadata commandMetadata { get; set; }
        public SignalServiceEndpoint signalServiceEndpoint { get; set; }
    }

    public class TopbarMenuButtonRenderer
    {
        public Icon icon { get; set; }
        public MenuRenderer menuRenderer { get; set; }
        public string trackingParams { get; set; }
        public Accessibility accessibility { get; set; }
        public string tooltip { get; set; }
        public string style { get; set; }
        public string targetId { get; set; }
        public MenuRequest menuRequest { get; set; }
    }

    public class TopbarButton
    {
        public TopbarMenuButtonRenderer topbarMenuButtonRenderer { get; set; }
        public ButtonRenderer buttonRenderer { get; set; }
    }

    public class HotkeyAccessibilityLabel
    {
        public AccessibilityData accessibilityData { get; set; }
    }

    public class HotkeyDialogSectionOptionRenderer
    {
        public Label label { get; set; }
        public string hotkey { get; set; }
        public HotkeyAccessibilityLabel hotkeyAccessibilityLabel { get; set; }
    }

    public class Option
    {
        public HotkeyDialogSectionOptionRenderer hotkeyDialogSectionOptionRenderer { get; set; }
    }

    public class HotkeyDialogSectionRenderer
    {
        public Title title { get; set; }
        public List<Option> options { get; set; }
    }

    public class DismissButton
    {
        public ButtonRenderer buttonRenderer { get; set; }
    }

    public class HotkeyDialogRenderer
    {
        public Title title { get; set; }
        public List<Section> sections { get; set; }
        public DismissButton dismissButton { get; set; }
        public string trackingParams { get; set; }
    }

    public class HotkeyDialog
    {
        public HotkeyDialogRenderer hotkeyDialogRenderer { get; set; }
    }

    public class SignalAction
    {
        public string signal { get; set; }
    }

    public class BackButton
    {
        public ButtonRenderer buttonRenderer { get; set; }
    }

    public class ForwardButton
    {
        public ButtonRenderer buttonRenderer { get; set; }
    }

    public class A11ySkipNavigationButton
    {
        public ButtonRenderer buttonRenderer { get; set; }
    }

    public class PlaceholderHeader
    {
        public List<Run> runs { get; set; }
    }

    public class PromptHeader
    {
        public List<Run> runs { get; set; }
    }

    public class ExampleQuery1
    {
        public List<Run> runs { get; set; }
    }

    public class ExampleQuery2
    {
        public List<Run> runs { get; set; }
    }

    public class PromptMicrophoneLabel
    {
        public List<Run> runs { get; set; }
    }

    public class LoadingHeader
    {
        public List<Run> runs { get; set; }
    }

    public class ConnectionErrorHeader
    {
        public List<Run> runs { get; set; }
    }

    public class ConnectionErrorMicrophoneLabel
    {
        public List<Run> runs { get; set; }
    }

    public class PermissionsHeader
    {
        public List<Run> runs { get; set; }
    }

    public class PermissionsSubtext
    {
        public List<Run> runs { get; set; }
    }

    public class DisabledHeader
    {
        public List<Run> runs { get; set; }
    }

    public class DisabledSubtext
    {
        public List<Run> runs { get; set; }
    }

    public class MicrophoneButtonAriaLabel
    {
        public List<Run> runs { get; set; }
    }

    public class ExitButton
    {
        public ButtonRenderer buttonRenderer { get; set; }
    }

    public class MicrophoneOffPromptHeader
    {
        public List<Run> runs { get; set; }
    }

    public class VoiceSearchDialogRenderer
    {
        public PlaceholderHeader placeholderHeader { get; set; }
        public PromptHeader promptHeader { get; set; }
        public ExampleQuery1 exampleQuery1 { get; set; }
        public ExampleQuery2 exampleQuery2 { get; set; }
        public PromptMicrophoneLabel promptMicrophoneLabel { get; set; }
        public LoadingHeader loadingHeader { get; set; }
        public ConnectionErrorHeader connectionErrorHeader { get; set; }
        public ConnectionErrorMicrophoneLabel connectionErrorMicrophoneLabel { get; set; }
        public PermissionsHeader permissionsHeader { get; set; }
        public PermissionsSubtext permissionsSubtext { get; set; }
        public DisabledHeader disabledHeader { get; set; }
        public DisabledSubtext disabledSubtext { get; set; }
        public MicrophoneButtonAriaLabel microphoneButtonAriaLabel { get; set; }
        public ExitButton exitButton { get; set; }
        public string trackingParams { get; set; }
        public MicrophoneOffPromptHeader microphoneOffPromptHeader { get; set; }
    }

    public class VoiceSearchButton
    {
        public ButtonRenderer buttonRenderer { get; set; }
    }

    public class DesktopTopbarRenderer
    {
        public Logo logo { get; set; }
        public Searchbox searchbox { get; set; }
        public string trackingParams { get; set; }
        public Interstitial interstitial { get; set; }
        public string countryCode { get; set; }
        public List<TopbarButton> topbarButtons { get; set; }
        public HotkeyDialog hotkeyDialog { get; set; }
        public BackButton backButton { get; set; }
        public ForwardButton forwardButton { get; set; }
        public A11ySkipNavigationButton a11ySkipNavigationButton { get; set; }
        public VoiceSearchButton voiceSearchButton { get; set; }
    }

    public class Topbar
    {
        public DesktopTopbarRenderer desktopTopbarRenderer { get; set; }
    }

public class Contents
{
    public TwoColumnSearchResultsRenderer twoColumnSearchResultsRenderer { get; set; }
}

    public class Root
    {
        public ResponseContext responseContext { get; set; }
        public string estimatedResults { get; set; }
        public Contents contents { get; set; }
        public string trackingParams { get; set; }
        public Topbar topbar { get; set; }
        public List<string> refinements { get; set; }
    }

