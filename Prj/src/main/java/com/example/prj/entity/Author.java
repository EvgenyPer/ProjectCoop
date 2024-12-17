package com.example.prj.entity;

// Импорт аннотаций Jakarta Persistence для работы с JPA
import jakarta.persistence.*;

// Аннотация @Entity указывает, что данный класс является сущностью JPA
@Entity
// Аннотация @Table указывает, что сущность будет сохраняться в таблицу с названием "authors"
@Table(name = "authors")
public class Author {

 // Аннотация @Id указывает, что это первичный ключ
 // Аннотация @GeneratedValue задает стратегию генерации значения для первичного ключа (IDENTITY)
 @Id
 @GeneratedValue(strategy = GenerationType.IDENTITY)
 private Long id;

 // Аннотация @Column задает имя столбца "first_name" и запрещает null-значения
 @Column(name = "first_name", nullable = false)
 private String firstName;

 // Аннотация @Column задает имя столбца "last_name" и запрещает null-значения
 @Column(name = "last_name", nullable = false)
 private String lastName;

 // Пустой конструктор нужен для JPA
 public Author() {}

 // Конструктор с параметрами для инициализации объекта
 public Author(String firstName, String lastName) {
  this.firstName = firstName;
  this.lastName = lastName;
 }

 // Геттер для id
 public Long getId() {
  return id;
 }

 // Сеттер для id
 public void setId(Long id) {
  this.id = id;
 }

 // Геттер для firstName
 public String getFirstName() {
  return firstName;
 }

 // Сеттер для firstName
 public void setFirstName(String firstName) {
  this.firstName = firstName;
 }

 // Геттер для lastName
 public String getLastName() {
  return lastName;
 }

 // Сеттер для lastName
 public void setLastName(String lastName) {
  this.lastName = lastName;
 }
}
