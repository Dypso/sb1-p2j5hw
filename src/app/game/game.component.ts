import { Component, OnInit } from "@angular/core";

interface Question {
  text: string;
  options: string[];
  correctAnswer: string;
}

@Component({
  selector: "Game",
  templateUrl: "./game.component.html"
})
export class GameComponent implements OnInit {
  currentQuestion: Question;
  score: number = 0;
  questions: Question[] = [
    {
      text: "Quelle est la capitale de la France ?",
      options: ["Paris", "Londres", "Berlin", "Madrid"],
      correctAnswer: "Paris"
    },
    {
      text: "Quel est le plus grand océan du monde ?",
      options: ["Atlantique", "Indien", "Arctique", "Pacifique"],
      correctAnswer: "Pacifique"
    }
  ];

  ngOnInit() {
    this.loadNextQuestion();
  }

  loadNextQuestion() {
    if (this.questions.length > 0) {
      const randomIndex = Math.floor(Math.random() * this.questions.length);
      this.currentQuestion = this.questions[randomIndex];
      this.questions.splice(randomIndex, 1);
    } else {
      // Fin du jeu
      alert(`Jeu terminé ! Votre score : ${this.score}`);
    }
  }

  onAnswer(selectedAnswer: string) {
    if (selectedAnswer === this.currentQuestion.correctAnswer) {
      this.score++;
      alert("Bonne réponse !");
    } else {
      alert(`Mauvaise réponse. La bonne réponse était : ${this.currentQuestion.correctAnswer}`);
    }
    this.loadNextQuestion();
  }
}