public interface IOptionItemUI
{
    void Initialize(OptionItemData data, object currentValue);
    bool IsModified(); // �⺻���� ���簪 ��
    void ResetToDefault(); // �ǵ����� ��ư ��� ȣ��
}