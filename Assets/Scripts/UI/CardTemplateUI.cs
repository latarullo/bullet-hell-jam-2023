using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardTemplateUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI lblTitle;
    [SerializeField] private TextMeshProUGUI lblDescription;
    [SerializeField] private Image imgCardImage;

    private void Awake() {
        this.gameObject.SetActive(false);
    }

    public void SetCardSO(CardSO cardSO) {
        this.gameObject.SetActive(true);
        this.lblTitle.text = cardSO.title;
        this.lblDescription.text = cardSO.description;
        this.imgCardImage.sprite = cardSO.image;
    }
}
