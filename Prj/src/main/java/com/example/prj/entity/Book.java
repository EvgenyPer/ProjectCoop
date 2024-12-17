package com.example.prj.entity;

import jakarta.persistence.*;
import java.math.BigDecimal;
import java.time.LocalDate;

// Аннотация @Entity указывает, что данный класс является сущностью JPA и будет отображаться в таблице базы данных.
@Entity
// Аннотация @Table задает имя таблицы в базе данных, связанной с этой сущностью.
@Table(name = "books")
public class Book {

    // Поле id будет использоваться как первичный ключ в таблице.
    @Id
    // Аннотация @GeneratedValue задает стратегию автоматической генерации значения для id (IDENTITY позволяет базе данных управлять идентификаторами).
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    // Поле name отображает название книги. Оно обязательно для заполнения (nullable = false).
    @Column(nullable = false)
    private String name;

    // Поле author — связь "многие к одному" с сущностью Author.
    // @JoinColumn задает имя столбца в таблице базы данных для хранения внешнего ключа.
    @ManyToOne
    @JoinColumn(name = "author_id", nullable = false)
    private Author author;

    // Поле releaseDate хранит дату выпуска книги. Это обязательное поле.
    @Column(name = "release_date", nullable = false)
    private LocalDate releaseDate;

    // Поле description хранит описание книги. columnDefinition = "TEXT" указывает, что данные будут храниться в формате текста (большого размера).
    @Column(nullable = false, columnDefinition = "TEXT")
    private String description;

    // Поле penalty — это сумма штрафа за потерю или задержку книги. Обязательно для заполнения.
    @Column(nullable = false)
    private BigDecimal penalty;

    // Поле count хранит количество экземпляров книги. Обязательно для заполнения.
    @Column(nullable = false)
    private Integer count;

    // Поле genreId используется для хранения идентификатора жанра. Обязательно для заполнения.
    @Column(name = "genre_id", nullable = false)
    private Long genreId;

    // Конструктор без параметров, необходим для работы JPA.
    public Book() {}

    // Конструктор с параметрами для создания объекта книги.
    public Book(String name, Author author, LocalDate releaseDate, String description, BigDecimal penalty, Integer count, Long genreId) {
        this.name = name;
        this.author = author;
        this.releaseDate = releaseDate;
        this.description = description;
        this.penalty = penalty;
        this.count = count;
        this.genreId = genreId;
    }

    // Геттеры и сеттеры предоставляют доступ к полям объекта и позволяют их изменять.
    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public Author getAuthor() {
        return author;
    }

    public void setAuthor(Author author) {
        this.author = author;
    }

    public LocalDate getReleaseDate() {
        return releaseDate;
    }

    public void setReleaseDate(LocalDate releaseDate) {
        this.releaseDate = releaseDate;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public BigDecimal getPenalty() {
        return penalty;
    }

    public void setPenalty(BigDecimal penalty) {
        this.penalty = penalty;
    }

    public Integer getCount() {
        return count;
    }

    public void setCount(Integer count) {
        this.count = count;
    }

    public Long getGenreId() {
        return genreId;
    }

    public void setGenreId(Long genreId) {
        this.genreId = genreId;
    }
}
