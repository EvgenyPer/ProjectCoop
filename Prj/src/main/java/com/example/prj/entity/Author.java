package com.example.prj.entity;

import jakarta.persistence.*;

@Entity
@Table(name = "authors")
public class Author {

 @Id
 @GeneratedValue(strategy = GenerationType.IDENTITY)
 private Long id;

 @Column(name = "first_name", nullable = false)
 private String firstName;

 @Column(name = "last_name", nullable = false)
 private String lastName;

 // Конструкторы
 public Author() {}

 public Author(String firstName, String lastName) {
  this.firstName = firstName;
  this.lastName = lastName;
 }

 // Геттеры и сеттеры
 public Long getId() {
  return id;
 }

 public void setId(Long id) {
  this.id = id;
 }

 public String getFirstName() {
  return firstName;
 }

 public void setFirstName(String firstName) {
  this.firstName = firstName;
 }

 public String getLastName() {
  return lastName;
 }

 public void setLastName(String lastName) {
  this.lastName = lastName;
 }
}