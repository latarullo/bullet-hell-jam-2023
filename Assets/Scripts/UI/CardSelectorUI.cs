using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardSelectorUI : MonoBehaviour {
    [SerializeField] private Transform cardGroup;
    [SerializeField] private Transform cardTemplate;
    [SerializeField] private CardSO[] cardSOs;

    // Start is called before the first frame update
    void Start() {
        this.Hide();
        this.Show();
    }

    // Update is called once per frame
    void Update() {

    }

    public void Hide() {
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void Show() {
        this.gameObject.SetActive(true);
        Time.timeScale = 0;
        UpdateVisual();
    }

    private void UpdateVisual() {
        foreach (Transform child in cardGroup) {
            if (child == cardTemplate) {
                continue;
            }
            Destroy(child.gameObject);
        }

        List<CardSO> cards = cardSOs.ToList();

        foreach (CardSO cardSO in cards) {
            Transform recipeTransform = Instantiate(cardTemplate, cardGroup);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<CardTemplateUI>().SetCardSO(cardSO);
        }
    }
}
