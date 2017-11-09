pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                sh 'dotnet -h'
            }
        }
        stage('Test') {
            steps {
                echo 'Testing..'
            }
        }
        stage('Deploy') {
            steps {
                echo 'Deploying....'
            }
        }
    }
}