public interface IOptionItemUI
{
    void Initialize(OptionItemData data, object currentValue);
    bool IsModified(); // 기본값과 현재값 비교
    void ResetToDefault(); // 되돌리기 버튼 등에서 호출
}