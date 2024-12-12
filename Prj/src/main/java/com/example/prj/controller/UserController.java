//package com.example.prj.controller;
//
//import com.example.prj.entity.User;
//import com.example.prj.service.UserService;
//import org.springframework.http.ResponseEntity;
//import org.springframework.web.bind.annotation.*;
//
//import java.util.List;
//
//@RestController
//@RequestMapping("/users")
//public class UserController {
//    private final UserService userService;
//
//    public UserController(UserService userService) {
//        this.userService = userService;
//    }
//
//    @GetMapping
//    public List<User> getAllUsers() {
//        return userService.getAllUsers();
//    }
//
//    @GetMapping("/{id}")
//    public ResponseEntity<User> getUserById(@PathVariable Long id) {
//        User user = userService.getUserById(id);
//        if (user == null) {
//            return ResponseEntity.notFound().build();
//        }
//        return ResponseEntity.ok(user);
//    }
//
//    @PostMapping
//    public User createUser(@RequestBody User user) {
//        return userService.saveUser(user);
//    }
//
//    @PutMapping("/{id}")
//    public ResponseEntity<User> updateUser(@PathVariable Long id, @RequestBody User updatedUser) {
//        User existingUser = userService.getUserById(id);
//        if (existingUser == null) {
//            return ResponseEntity.notFound().build();
//        }
//
//        existingUser.setLogin(updatedUser.getLogin());
//        existingUser.setFirstName(updatedUser.getFirstName());
//        existingUser.setLastName(updatedUser.getLastName());
//        existingUser.setPassword(updatedUser.getPassword());
//        existingUser.setRole(updatedUser.getRole());
//
//        return ResponseEntity.ok(userService.saveUser(existingUser));
//    }
//
//    @DeleteMapping("/{id}")
//    public ResponseEntity<Void> deleteUser(@PathVariable Long id) {
//        if (userService.getUserById(id) == null) {
//            return ResponseEntity.notFound().build();
//        }
//        userService.deleteUser(id);
//        return ResponseEntity.noContent().build();
//    }
//}
