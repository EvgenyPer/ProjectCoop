package com.example.prj.entity;

import jakarta.persistence.*;

// Класс представляет сущность "Genre" (Жанр), связанную с таблицей в базе данных.
@Entity // Аннотация указывает, что этот класс является JPA-сущностью.
@Table(name = "genres") // Задает имя таблицы в базе данных для этой сущности.
public class Genre {

    @Id // Указывает, что поле является первичным ключом.
    @GeneratedValue(strategy = GenerationType.IDENTITY) // Стратегия автоинкремента для первичного ключа.
    private Long id; // Уникальный идентификатор жанра.

    @Column(name = "name", nullable = false) // Поле базы данных с именем "name", не допускающее null.
    private String name; // Название жанра.

    // Конструктор без параметров, необходим для работы JPA.
    public Genre() {}

    // Конструктор для создания объекта Genre с указанием названия.
    public Genre(String name) {
        this.name = name;
    }

    // Геттер для получения идентификатора жанра.
    public Long getId() {
        return id;
    }

    // Сеттер для установки идентификатора жанра.
    public void setId(Long id) {
        this.id = id;
    }

    // Геттер для получения названия жанра.
    public String getName() {
        return name;
    }

    // Сеттер для установки названия жанра.
    public void setName(String name) {
        this.name = name;
    }
}
