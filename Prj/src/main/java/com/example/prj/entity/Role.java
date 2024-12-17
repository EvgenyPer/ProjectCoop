package com.example.prj.entity;

import jakarta.persistence.*;

// Аннотация @Entity указывает, что этот класс является сущностью JPA (Java Persistence API),
// которая будет отображаться в таблицу базы данных.
@Entity
// Аннотация @Table указывает имя таблицы в базе данных, соответствующей этой сущности.
@Table(name = "roles")
public class Role {

    // Аннотация @Id обозначает поле как первичный ключ таблицы.
    @Id
    // Аннотация @GeneratedValue указывает стратегию генерации значений первичного ключа.
    // GenerationType.IDENTITY означает, что база данных автоматически генерирует значения (например, AUTO_INCREMENT).
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    // Аннотация @Column указывает имя колонки в таблице базы данных.
    // Поле не может быть пустым из-за nullable = false.
    @Column(name = "name", nullable = false)
    private String name;

    // Пустой конструктор необходим для работы JPA.
    public Role() {}

    // Конструктор с параметром для создания объекта с именем роли.
    public Role(String name) {
        this.name = name;
    }

    // Геттеры и сеттеры для доступа и модификации полей сущности.

    // Возвращает идентификатор роли.
    public Long getId() {
        return id;
    }

    // Устанавливает идентификатор роли.
    public void setId(Long id) {
        this.id = id;
    }

    // Возвращает имя роли.
    public String getName() {
        return name;
    }

    // Устанавливает имя роли.
    public void setName(String name) {
        this.name = name;
    }
}
