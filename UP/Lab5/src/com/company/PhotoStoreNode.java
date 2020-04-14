package com.company;

public class PhotoStoreNode {

    private String shopPart, genre, name;
    private int price;

    //PhotoStoreNode () {}
    PhotoStoreNode(String shopPart, String genre, String name, String price) {
        this.shopPart = shopPart;
        this.genre = genre;
        this.name = name;
        try {
            this.price = Integer.parseInt(price);
        } catch (Exception e) {
            this.price = -1;
        }
    }

    public String getShopPart() {
        return shopPart;
    }

    public String getGenre() {
        return genre;
    }

    public String getName() {
        return name;
    }

    public int getPrice() {
        return price;
    }

}